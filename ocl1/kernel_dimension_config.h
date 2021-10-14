#pragma once

class kernel_dimension_config
{
public:
	kernel_dimension_config(int size, size_t* local_size, size_t* global_size);

	int size;
	size_t* local_size;
	size_t* global_size;
};
