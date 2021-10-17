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

matrix::matrix(float* current_matrix, size_t column_count, size_t row_count)
{
	std::vector<std::vector<float>> result(row_count);

	for (size_t row = 0; row < row_count; row++)
	{
		std::vector<float> local_result(column_count);

		for (size_t column = 0; column < column_count; column++)
		{
			local_result[column] = current_matrix[row * row_count + column];
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
