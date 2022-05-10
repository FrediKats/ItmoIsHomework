#pragma once

#include <string>

#include "program_builder.h"

namespace ocl1
{
	class kernel_source
	{
	public:
		virtual cl_kernel get_kernel_source_code(std::string kernel_name, program_builder builder) = 0;

		virtual ~kernel_source() = default;
	};
}
