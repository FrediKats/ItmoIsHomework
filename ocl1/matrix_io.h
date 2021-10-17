#pragma once
#include <fstream>

#include "matrix.h"
#include "matrix_multiplication_context.h"

class matrix_io
{
public:
	matrix_multiplication_context parse_file(const std::string& file_path)
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

	void write_matrix(matrix matrix, std::string file_path)
	{
		std::ofstream file_descriptor(file_path);
		if (!file_descriptor.good())
			throw std::exception("Can not open file");

		try
		{
			auto data = matrix.data;

			file_descriptor << data.size() << data[0].size();

			for (size_t row = 0; row < data.size(); row++)
			{
				for (size_t column = 0; column < data[row].size(); column++)
				{
					auto value = data[row][column];
					file_descriptor << value;
					if (column < data[row].size() - 1)
						file_descriptor << " ";
				}
				if (row < data.size() - 1)
				{
					file_descriptor << std::endl;
				}
			}
		}
		catch (...)
		{
			file_descriptor.close();
			throw;
		}
	}
};
