#pragma once
#include <fstream>

#include "matrix.h"
#include "matrix_multiplication_context.h"

class matrix_provider
{
public:
	matrix_multiplication_context matrix_provider::parse_file(const std::string& file_path)
	{
		std::ifstream file_descriptor(file_path);
		if (!file_descriptor.good())
			throw std::exception("Can not open file");

		try
		{
			size_t n, m, k;
			file_descriptor >> n >> m >> k;

			const matrix a = read_matrix(m, k, file_descriptor);
			const matrix b = read_matrix(k, n, file_descriptor);
			return matrix_multiplication_context(n, k, m, a, b);
		}
		catch (...)
		{
			file_descriptor.close();
			throw;
		}
	}

	matrix read_matrix(size_t row_count, size_t column_count, std::ifstream& file_descriptor)
	{
		std::vector<std::vector<float>> result(row_count * column_count);
		for (size_t row_index = 0; row_index < row_count; row_index++)
		{
			std::vector<float> temp(column_count);
			for (size_t column_index = 0; column_index < column_count; column_index++)
			{
				file_descriptor >> temp[column_index];
			}
			result[row_index] = temp;
		}
		return matrix(result);
	}
};
