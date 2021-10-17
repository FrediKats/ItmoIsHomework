#include "kernel_dimension_config.h"

kernel_dimension_config::kernel_dimension_config(int size, size_t* local_size, size_t* global_size):
	size(size),
	local_size(local_size),
	global_size(global_size)
{
}

