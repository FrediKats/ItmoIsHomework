#pragma once
#include <string>

#include "program_builder.h"

class kernel_source
{
public:
	virtual cl_kernel get_kernel_source_code(const execution_context execution_context_instance, std::string kernel_name, program_builder builder) = 0;

	virtual ~kernel_source() = default;
};
