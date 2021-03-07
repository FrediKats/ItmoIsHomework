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
		float determinant();
		float determinant(int parallel_thread_count);

	private:
		std::vector<std::vector<float>> data_;
	};
}
