#pragma once
#include "kernel_source.h"
#include "sum_kernel_argument.h"
#include "sum_kernel_response.h"

class sum_kernel
{
public:
	explicit sum_kernel(kernel_source& source, execution_context execution_context_instance);

	sum_kernel_response execute(sum_kernel_argument argument);

private:
	execution_context execution_context_instance_;
	cl_kernel kernel_;
};
