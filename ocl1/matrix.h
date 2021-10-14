#pragma once
#include <string>
#include <vector>

class matrix
{
public:
	explicit matrix(std::vector<std::vector<float>> data);
	std::string to_string();
	float* to_array() const
	{
		const size_t buffer_size = data_.size() * data_[0].size();
		float* result = new float[buffer_size];

		for (size_t row = 0; row < data_.size(); row++)
		{
			for (size_t column = 0; column < data_[row].size(); column++)
			{
				result[row * data_[row].size() + column] = data_[row][column];
				
			}
		}

		return result;
	}

protected:
	std::vector<std::vector<float>> data_;
};
