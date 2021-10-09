#pragma once
#include "sum_kernel.h"

class kernel_argument
{
public:
	virtual void write_arguments(execution_context execution_context_instance, cl_kernel kernel) = 0;
};
