#include "matrix_provider.h"

#include <ctime>
#include <fstream>

namespace lab1
{
	matrix_provider::matrix_provider()
	{
		srand(time(nullptr));
	}

	matrix matrix_provider::generate(const size_t size)
	{
		std::vector<std::vector<float>> result(size);
		for (size_t i = 0; i < size; i++)
		{
			std::vector<float> temp(size);
			for (size_t j = 0; j < size; j++)
			{
				//NB: https://stackoverflow.com/a/686373
				temp[j] = static_cast <float> (rand()) / static_cast <float> (RAND_MAX);
			}
			result[i] = temp;
		}
		return matrix(result);
	}

	matrix matrix_provider::parse_file(const std::string& file_path)
	{
		std::ifstream file_descriptor(file_path);
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

			file_descriptor.close();
			return matrix(result);
		}
		catch (...)
		{
			file_descriptor.close();
			throw;
		}
	}
}
