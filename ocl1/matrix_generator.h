#pragma once
#include "matrix.h"
#include "matrix_multiplication_context.h"

class matrix_generator
{
public:
	static matrix generate(const size_t size);
	static matrix_multiplication_context generate_context(size_t size)
	{
		const auto first = generate(size);
		const auto second = generate(size);

		return matrix_multiplication_context(size, size, size, first, second);
	}
};
