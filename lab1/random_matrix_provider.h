#pragma once
#include "matrix.h"

namespace lab1
{
	class random_matrix_provider
	{
	public:
		random_matrix_provider();
		static matrix generate(size_t size);
	};
}
