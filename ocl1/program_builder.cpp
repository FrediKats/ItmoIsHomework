#include "program_builder.h"

cl_program program_builder::build(cl_context context, const std::string& kernel_source_code)
{
	const char* kernel_source_code_link = kernel_source_code.c_str();
	int err;

	//TODO: can optimize this logic wit prebuild
	cl_program program = clCreateProgramWithSource(context, 1, &kernel_source_code_link, nullptr, &err);
	if (!program)
		throw std::exception("Error: Failed to create compute program");

	//TODO: handle error
	//TODO: clGetProgramBuildLog
	err = clBuildProgram(program, 0, nullptr, nullptr, nullptr, nullptr);
	if (!program)
		throw std::exception("Error: Failed to create compute program");

	return program;
}
