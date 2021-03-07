#include <chrono>
#include <fstream>
#include <iostream>
#include <string>

#include "matrix.h"
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

	const std::string file_path(argv[0]);
	return parse_file(file_path);
}

void f(lab1::matrix matrix, int thread_count)
{
	auto start = std::chrono::system_clock::now();
	std::cout << "Determinant: " << matrix.determinant(thread_count) << std::endl;
	auto end = std::chrono::system_clock::now();

	const std::chrono::duration<double> difference = end - start;

	std::cout << "\nTime (%i thread(s)): " << difference.count() * 1000 << " ms" << std::endl;
}

int main(int argc, char** argv)
{
	try
	{
		const auto matrix = create_matrix(argc, argv);

		for (int i = 1; i < 6; i++)
			f(matrix, i);
		
	}
	catch (std::exception& e)
	{
		std::cout << e.what();
	}
}
