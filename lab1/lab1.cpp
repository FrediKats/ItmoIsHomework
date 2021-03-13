#include <chrono>
#include <fstream>
#include <functional>
#include <iostream>
#include <omp.h>
#include <string>

#include "fast_matrix.h"
#include "fast_multithread_matrix.h"
#include "matrix.h"
#include "matrix_provider.h"

std::chrono::duration<double> benchmark_run(const std::function<void(void)>& functor)
{
	const auto run_count = 30;
	
	std::chrono::duration<double> result = std::chrono::duration<double>::zero();
	for (auto i = 0; i < run_count; i++)
	{
		const auto start = std::chrono::high_resolution_clock::now();
		functor();
		const auto end = std::chrono::high_resolution_clock::now();

		result += (end - start);
	}
	
	return result / run_count;
}

void common_run(int argc, char** argv)
{
	const std::string file_path(argv[1]);
	const int thread_count = atoi(argv[2]);
	const int optimal_thread_count = atoi(argv[2]);
	
	lab1::matrix matrix = lab1::matrix_provider::parse_file(file_path);
	lab1::fast_matrix fm = lab1::fast_matrix(matrix);
	auto multithread_matrix = lab1::fast_multithread_matrix(matrix, thread_count == 0 ? optimal_thread_count : thread_count);
	lab1::matrix* current = thread_count >= 0
		                        ? &multithread_matrix
		                        : &fm;

	const auto start = std::chrono::system_clock::now();
	const float result = current->determinant();
	const auto end = std::chrono::system_clock::now();
	
	const std::chrono::duration<double> difference = end - start;

	std::cout << "Determinant: " << result << std::endl;
	std::cout << "\nTime (" << thread_count << " thread(s)): " << difference.count() * 1000 << " ms" << std::endl;
}

void common_run()
{
	const std::string file_path("D:\\coding\\itmo-master\\LikeCtButCheaper\\lab1\\Debug\\data.txt");
	const int thread_count = 4;
	const int optimal_thread_count = -1;

	lab1::matrix matrix = lab1::matrix_provider::parse_file(file_path);
	lab1::fast_matrix fm = lab1::fast_matrix(matrix);
	auto multithread_matrix = lab1::fast_multithread_matrix(matrix, thread_count == 0 ? optimal_thread_count : thread_count);
	lab1::matrix* current = thread_count >= 0
		? &multithread_matrix
		: &fm;

	const auto start = std::chrono::system_clock::now();
	const float result = current->determinant();
	const auto end = std::chrono::system_clock::now();

	const std::chrono::duration<double> difference = end - start;

	std::cout << "Determinant: " << result << std::endl;
	std::cout << "\nTime (" << thread_count << " thread(s)): " << difference.count() * 1000 << " ms" << std::endl;
}

void benchmark_run()
{
	lab1::matrix matrix = lab1::matrix_provider::generate(300);
	auto slow_matrix = lab1::matrix(matrix);
	auto fast_matrix = lab1::fast_matrix(matrix);
	lab1::fast_multithread_matrix multithread_matrix = lab1::fast_multithread_matrix(matrix, 4);

	std::cout << fast_matrix.determinant() << std::endl;
	//std::cout << slow_matrix.determinant() << std::endl;
	std::cout << multithread_matrix.determinant_dynamic_schedule() << std::endl;
	std::cout << multithread_matrix.determinant_static_schedule() << std::endl;
	std::cout << multithread_matrix.determinant_guided_schedule() << std::endl;
	
	std::cout << "Single\t1\t" << benchmark_run([&fast_matrix] { fast_matrix.determinant(); }).count() * 1000 << std::endl;

	for (int i = 1; i <= omp_get_num_procs(); i++)
	{
		auto multithread_matrix = lab1::fast_multithread_matrix(matrix, i);

		std::cout << "Dynamic\t" << i << "\t" << benchmark_run([&multithread_matrix] { multithread_matrix.determinant_dynamic_schedule(); }).count() * 1000 << std::endl;
		std::cout << "Static\t" << i << "\t" << benchmark_run([&multithread_matrix] { multithread_matrix.determinant_static_schedule(); }).count() * 1000 << std::endl;
		std::cout << "Guided\t" << i << "\t" << benchmark_run([&multithread_matrix] { multithread_matrix.determinant_guided_schedule(); }).count() * 1000 << std::endl;
	}
	
	/*for (int i = 1; i <= omp_get_num_procs(); i++)
	{
		auto multithread_matrix = lab1::multithread_matrix(matrix, i);

		std::cout << "Dynamic\t" << i << "\t" << benchmark_run([&multithread_matrix] { multithread_matrix.determinant_dynamic_schedule(); }).count() * 1000 << std::endl;
		std::cout << "Static\t" << i << "\t" << benchmark_run([&multithread_matrix] { multithread_matrix.determinant_static_schedule(); }).count() * 1000 << std::endl;
		std::cout << "Guided\t" << i << "\t" << benchmark_run([&multithread_matrix] { multithread_matrix.determinant_guided_schedule(); }).count() * 1000 << std::endl;
	}*/
}

int main(int argc, char** argv)
{
	try
	{
		//NB: exec_path file_path thread_count
		if (argc == 3)
		{
			common_run(argc, argv);
			return 0;
		}
		//NB: for test propose
		//else if (argc == 1)
		//{
		//	benchmark_run();
		//	//common_run();
		//	return 0;
		//}
		else
		{
			std::cout << "Unexpected argument count";
			return 1;
		}
	}
	catch (std::exception& e)
	{
		std::cout << "Unexpected error.\n" << e.what();
		return 1;
	}
	catch (...)
	{
		std::cout << "Unexpected error.";
		return 1;
	}
}
