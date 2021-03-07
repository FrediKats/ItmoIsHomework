#include "matrix.h"

#include <ostream>
#include <sstream>

namespace lab1
{
	matrix::matrix(std::vector<std::vector<float>> data) : data_(std::move(data))
	{
		//TODO: ensure is NxN size
	}

	std::string matrix::to_string()
	{
				std::stringstream string_builder;

		for (auto& row : data_)
		{
			for (float j : row)
			{
				//NB: I will take is as a string builder. https://stackoverflow.com/a/2462985
				string_builder << std::to_string(j) << " ";
			}
			string_builder << std::endl;
		}

		return string_builder.str();
	}

	float matrix::determinant()
	{
		if (data_.size() == 1)
			return data_[0][0];

		float result = 0;
		
		for (size_t row_index = 0; row_index < data_.size(); row_index++)
		{
			for (size_t column_index = 0; column_index < data_[row_index].size(); column_index++)
			{
				result += pow(-1, row_index + column_index) * data_[row_index][column_index] * get_minor(column_index, row_index).determinant();
			}
		}
		
		return result;
	}

	matrix matrix::get_minor(const size_t excluded_column, const size_t excluded_row)
	{
		std::vector<std::vector<float>> result(data_.size() - 1);

		
		for (size_t row_index = 0; row_index < data_.size(); row_index++)
		{
			if (excluded_row == row_index)
				continue;

			std::vector<float> temp(data_.size() - 1);
			for (size_t column_index = 0; column_index < data_[row_index].size(); column_index++)
			{
				if (excluded_column == column_index)
					continue;

				temp[column_index - (column_index >= excluded_column)] = data_[row_index][column_index];
				
			}
			result[row_index - (row_index >= excluded_row)] = temp;
		}

		return lab1::matrix(result);
	}
}
