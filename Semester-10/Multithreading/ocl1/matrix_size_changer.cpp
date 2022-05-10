#include "matrix_size_changer.h"

matrix_size_changer::matrix_size_changer(matrix_multiplication_context original_context, size_t new_size): new_size(new_size),
	original_context_(original_context),
	modified_context_(
		new_size,
		new_size,
		new_size,
		original_context.first.resize(new_size, new_size),
		original_context.second.resize(new_size, new_size))
{
}
