#pragma once
#include "execution_context.h"

class sum_kernel_response
{
public:
	int c;

	sum_kernel_response();

	void setup(execution_context execution_context_instance, cl_kernel kernel);
	void read_result(execution_context execution_context_instance);

private:
	cl_mem output_memory_;
};
