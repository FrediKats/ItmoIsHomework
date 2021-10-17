#pragma once
#include "kernel_dimension_config.h"
#include "matrix.h"

class matrix_multiplication_context
{
public:
	const size_t second_matrix_width;
	const size_t k;
	const size_t first_matrix_height;
	const matrix first;
	const matrix second;

	matrix_multiplication_context(
		const size_t second_matrix_width,
		const size_t k,
		const size_t first_matrix_height,
		const matrix& first,
		const matrix& second)
		: second_matrix_width(second_matrix_width),
		  k(k),
		  first_matrix_height(first_matrix_height),
		  first(first),
		  second(second)
	{
	}

	ocl1::kernel_dimension_config create_config()
	{
		return ocl1::kernel_dimension_config(2, nullptr, new size_t[2] { first_matrix_height, second_matrix_width});
	}
};
