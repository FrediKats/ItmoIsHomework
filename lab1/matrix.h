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

	private:
		std::vector<std::vector<float>> data_;
	};
}
