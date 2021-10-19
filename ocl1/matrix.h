#pragma once

#include <string>
#include <vector>

class matrix
{
public:
	explicit matrix(std::vector<std::vector<float>> data);
	explicit matrix(float* current_matrix, size_t row_count, size_t column_count);

	std::string to_string();
	float* to_array() const;
	matrix resize(size_t new_height, size_t new_weight) const;

public:
	std::vector<std::vector<float>> data;
};
