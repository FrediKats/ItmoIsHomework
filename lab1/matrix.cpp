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

	matrix matrix::get_minor(size_t x, size_t y)
	{
		std::vector<std::vector<float>> result(data_.size() - 1);

		
		for (size_t row_index = 0; row_index < data_.size(); row_index++)
		{
			if (y == row_index)
				continue;

			std::vector<float> temp(data_.size() - 1);
			for (size_t column_index = 0; column_index < data_[row_index].size(); column_index++)
			{
				if (x == column_index)
					continue;

				temp[column_index - (column_index >= x)] = data_[row_index][column_index];
				
			}
			result[row_index - (row_index >= y)] = temp;
		}

		return lab1::matrix(result);
	}
}
