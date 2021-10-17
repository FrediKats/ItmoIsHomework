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
			size_t second_matrix_width, first_matrix_height, k;
			file_descriptor >> second_matrix_width >> k >> first_matrix_height;

			const matrix first = read_matrix(first_matrix_height, k, file_descriptor);
			const matrix second = read_matrix(k, second_matrix_width, file_descriptor);
			return matrix_multiplication_context(second_matrix_width, k, first_matrix_height, first, second);
		}
		catch (...)
		{
			file_descriptor.close();
			throw;
		}
	}

	matrix read_matrix(const size_t height, const size_t width, std::ifstream& file_descriptor)
	{
		std::vector<std::vector<float>> result(height * width);
		for (size_t row_index = 0; row_index < height; row_index++)
		{
			std::vector<float> temp(width);
			for (size_t column_index = 0; column_index < width; column_index++)
			{
				file_descriptor >> temp[column_index];
			}
			result[row_index] = temp;
		}
		return matrix(result);
	}

	void write_matrix(const matrix& matrix, const std::string file_path)
	{
		std::ofstream file_descriptor(file_path);
		if (!file_descriptor.good())
			throw std::exception("Can not open file");

		const auto data = matrix.data;
		file_descriptor << data[0].size() << " " << data.size() << std::endl;

		for (size_t row = 0; row < data.size(); row++)
		{
			for (size_t column = 0; column < data[row].size(); column++)
			{
				file_descriptor << data[row][column];
				if (column < data[row].size() - 1)
					file_descriptor << " ";
			}
			if (row < data.size() - 1)
			{
				file_descriptor << std::endl;
			}
		}
	}
};
