#pragma once
#include "matrix_multiplication_context.h"

class matrix_size_changer
{
public:
	matrix_size_changer(matrix_multiplication_context original_context, size_t new_size);

	size_t new_size;
	matrix_multiplication_context original_context_;
	matrix_multiplication_context modified_context_;
};
