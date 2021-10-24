#pragma once

#include "matrix_multiplication_context.h"

inline size_t bound(size_t value)
{
    int TS = 32;
    return value / TS * TS + (value % TS != 0) * TS;
}

inline size_t max_bound(size_t a, size_t b, size_t c)
{
    return bound(std::max(a, std::max(b, c)));
}

inline size_t max_bound(matrix_multiplication_context context)
{
    return max_bound(context.first_matrix_height, context.k, context.second_matrix_width);
}

class matrix_extensions
{
public:
	
};
