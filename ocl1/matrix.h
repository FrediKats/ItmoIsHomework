#pragma once

#include <string>
#include <vector>

class matrix
{
public:
	explicit matrix(std::vector<std::vector<float>> data);
	explicit matrix(float* current_matrix, size_t column_count, size_t row_count);

	std::string to_string();
	float* to_array() const
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

public:
	std::vector<std::vector<float>> data;
};
