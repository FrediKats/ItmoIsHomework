#pragma once
#include "kernel_dimension_config.h"
#include "matrix.h"

class matrix_multiplication_context
{
public:
	const size_t n;
	const size_t k;
	const size_t m;
	const matrix first;
	const matrix second;

	matrix_multiplication_context(
		const size_t n,
		const size_t k,
		const size_t m,
		const matrix& first,
		const matrix& second)
		: n(n),
		  k(k),
		  m(m),
		  first(first),
		  second(second)
	{
	}

	kernel_dimension_config create_config()
	{
		return kernel_dimension_config(2, nullptr, new size_t[2] { n, m});
	}
};
