#pragma once
#include "matrix.h"

class matrix_multiplication_context
{
public:
	const size_t n;
	const size_t k;
	const size_t m;
	const matrix first;
	const matrix second;
	matrix result;

	matrix_multiplication_context(const size_t n, const size_t k, const size_t m, matrix first, matrix second)
		: n(n),
		  k(k),
		  m(m),
		  first(std::move(first)),
		  second(std::move(second))
	{
	}
};
