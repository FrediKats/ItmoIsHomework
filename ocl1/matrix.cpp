#include "matrix.h"

#include <ostream>
#include <sstream>

matrix::matrix(std::vector<std::vector<float>> data) : data(std::move(data))
{
	for (auto vector : data)
	{
		if (vector.size() != data.size())
			throw std::exception("Matrix must be NxN size");
	}
}

matrix::matrix(float* current_matrix, size_t row_count, size_t column_count)
{
	std::vector<std::vector<float>> result(row_count);

	for (size_t row = 0; row < row_count; row++)
	{
		std::vector<float> local_result(column_count);

		for (size_t column = 0; column < column_count; column++)
		{
			local_result[column] = current_matrix[row * column_count + column];
		}

		result[row] = local_result;
	}

	data = result;
}

std::string matrix::to_string()
{
	std::stringstream string_builder;

	for (auto& row : data)
	{
		for (float j : row)
			//NB: I will take is as a string builder. https://stackoverflow.com/a/2462985
			string_builder << std::to_string(j) << " ";
		string_builder << std::endl;
	}

	return string_builder.str();
}

float* matrix::to_array() const
{
	const size_t buffer_size = data.size() * data[0].size();
	float* result = new float[buffer_size];

	for (size_t row = 0; row < data.size(); row++)
	{
		for (size_t column = 0; column < data[row].size(); column++)
		{
			result[row * data[row].size() + column] = data[row][column];
				
		}
	}

	return result;
}

matrix matrix::resize(size_t new_height, size_t new_weight) const
{
	std::vector<std::vector<float>> result(new_height);

	for (size_t row = 0; row < new_height; row++)
	{
		std::vector<float> local_result(new_weight);

		for (size_t column = 0; column < new_weight; column++)
		{
			if (data.size() <= row || data[row].size() <= column)
			{
				local_result[column] = 0;
			}
			else
			{
				local_result[column] = data[row][column];
			}
		}

		result[row] = local_result;
	}

	return matrix(result);
}
