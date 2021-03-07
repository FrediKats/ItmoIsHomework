#include <chrono>
#include <fstream>
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

lab1::matrix create_matrix(int argc, char** argv)
{
	if (argc <= 1)
		return lab1::random_matrix_provider::generate(6);

	const std::string file_path(argv[1]);
	return parse_file(file_path);
}

std::chrono::duration<double> bench_result(lab1::matrix matrix, int thread_count)
{
	lab1::matrix* current = &matrix;
	if (thread_count == 0)
	{
		thread_count = omp_get_num_procs();
	}
	
	auto t = lab1::multithread_matrix(matrix, thread_count);

	if (thread_count > 0)
	{
		current = &t;
	}

	float result;
	auto start = std::chrono::system_clock::now();
	result = current->determinant();
	auto end = std::chrono::system_clock::now();

	const std::chrono::duration<double> difference = end - start;

	std::cout << "Determinant: " << result << std::endl;
	std::cout << "\nTime (" << thread_count << " thread(s)): " << difference.count() * 1000 << " ms" << std::endl;
	return difference;
}

int main(int argc, char** argv)
{
	try
	{
		auto matrix = create_matrix(argc, argv);
		for (int i = -1; i < 8; i++)
		{
			bench_result(matrix, i);
		}
	}
	catch (std::exception& e)
	{
		std::cout << e.what();
	}
}
