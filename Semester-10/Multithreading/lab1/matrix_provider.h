#pragma once
#include "matrix.h"

namespace lab1
{
	class matrix_provider
	{
	public:
		matrix_provider();
		static matrix generate(size_t size);
		static matrix parse_file(const std::string& file_path);
	};
}
