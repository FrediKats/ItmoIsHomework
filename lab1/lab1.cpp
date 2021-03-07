#include <chrono>
#include <fstream>
#include <functional>
#include <iostream>
#include <omp.h>
#include <string>

#include "matrix.h"
#include "multithread_matrix.h"
#include "random_matrix_provider.h"

lab1::matrix parse_file(const std::string& file_path)
{
	std::ifstream file_descriptor(file_path.c_str());
	if (!file_descriptor.good())
		throw std::exception("Can not open file");

	try
	{
		size_t size;
		file_descriptor >> size;
		std::vector<std::vector<float>> result(size);

		for (size_t row_index = 0; row_index < size; row_index++)
		{
			std::vector<float> temp(size);
			for (size_t column_index = 0; column_index < size; column_index++)
			{
				file_descriptor >> temp[column_index];
			}
			result[row_index] = temp;
		}

		return lab1::matrix(result);
	}
	catch (...)
	{
		file_descriptor.close();
		throw;
	}
}

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
	
	lab1::matrix matrix = parse_file(file_path);
	auto multithread_matrix = lab1::multithread_matrix(matrix, thread_count == 0 ? omp_get_num_procs() : thread_count);
	lab1::matrix* current = thread_count > 0
		                        ? &multithread_matrix
		                        : &matrix;

	const auto start = std::chrono::system_clock::now();
	const float result = current->determinant();
	const auto end = std::chrono::system_clock::now();
	
	const std::chrono::duration<double> difference = end - start;

	std::cout << "Determinant: " << result << std::endl;
	std::cout << "\nTime (" << thread_count << " thread(s)): " << difference.count() * 1000 << " ms" << std::endl;
}

void benchmark_run()
{
	lab1::matrix matrix = lab1::random_matrix_provider::generate(6);

	std::cout << "Single thread:\t" << benchmark_run([&matrix] { matrix.determinant(); }).count() * 1000 << " ms" << std::endl;
	for (int i = 1; i <= omp_get_num_procs(); i++)
	{
		auto multithread_matrix = lab1::multithread_matrix(matrix, i);

		std::cout << "Dynamic\t" << i << " thread(s))\t" << benchmark_run([&multithread_matrix] { multithread_matrix.determinant_dynamic_schedule(); }).count() * 1000 << " ms" << std::endl;
		std::cout << "Static\t" << i << " thread(s))\t" << benchmark_run([&multithread_matrix] { multithread_matrix.determinant_static_schedule(); }).count() * 1000 << " ms" << std::endl;
		std::cout << "Guided\t" << i << " thread(s))\t" << benchmark_run([&multithread_matrix] { multithread_matrix.determinant_guided_schedule(); }).count() * 1000 << " ms" << std::endl;
	}
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
		else if (argc == 1)
		{
			benchmark_run();
			return 0;
		}
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
