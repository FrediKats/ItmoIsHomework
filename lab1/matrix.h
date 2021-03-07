#pragma once
#include <string>
#include <vector>

namespace lab1
{
	class matrix
	{
	public:
		explicit matrix(std::vector<std::vector<float>> data);
		std::string to_string();
		matrix get_minor(size_t excluded_column, size_t excluded_row);
		virtual float determinant();

	protected:
		std::vector<std::vector<float>> get_minor_as_vector(size_t excluded_column, size_t excluded_row);
		std::vector<std::vector<float>> data_;
	};
}
