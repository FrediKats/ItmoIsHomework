#pragma once
#include <string>
#include <CL/cl.h>

class program_builder
{
public:
	cl_program build(cl_context context, const std::string& kernel_source_code);
};
