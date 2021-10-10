#pragma once
#include "execution_context.h"

class kernel_argument
{
public:
	virtual void write_arguments(execution_context execution_context_instance, cl_kernel kernel) = 0;

	virtual ~kernel_argument() = default;
};
