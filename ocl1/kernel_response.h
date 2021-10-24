#pragma once
#include "execution_context.h"

namespace ocl1
{
	class kernel_response
	{
	public:
		virtual void setup(execution_context execution_context_instance, cl_kernel kernel) = 0;
		virtual void read_result(execution_context execution_context_instance) = 0;

		virtual ~kernel_response() = default;
	};
}
