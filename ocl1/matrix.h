#pragma once

#include <string>
#include <vector>

class matrix
{
public:
	explicit matrix(std::vector<std::vector<float>> data);
	explicit matrix(float* current_matrix, size_t column_count, size_t row_count);

	std::string to_string();
	float* to_array() const;

public:
	std::vector<std::vector<float>> data;
};
