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
}