#pragma once
#include "execution_context.h"

namespace ocl1
{
	class kernel_argument
	{
	public:
		virtual void write_arguments(execution_context execution_context_instance, cl_kernel kernel) = 0;

		virtual ~kernel_argument() = default;
	};
}
