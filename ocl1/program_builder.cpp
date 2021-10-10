#include "program_builder.h"

#include <iostream>

#include "error_message.h"
#include "error_validator.h"

cl_program program_builder::build(cl_context context, const std::string& kernel_source_code, const device device)
{
	const char* kernel_source_code_link = kernel_source_code.c_str();
	int err;

	//TODO: can optimize this logic wit prebuild
	cl_program program = clCreateProgramWithSource(context, 1, &kernel_source_code_link, nullptr, &err);
	validate_error(err).or_throw(error_message::about_create_program);
	if (!program)
		throw std::exception("Error: Failed to create compute program");

	err = clBuildProgram(program, 0, nullptr, nullptr, nullptr, nullptr);
	if (err != CL_SUCCESS)
	{
		size_t len = 0;
		cl_int ret = CL_SUCCESS;
		ret = clGetProgramBuildInfo(program, nullptr, CL_PROGRAM_BUILD_LOG, 0, nullptr, &len);
		char* buffer = static_cast<char*>(calloc(len, sizeof(char)));
		ret = clGetProgramBuildInfo(program, device.id, CL_PROGRAM_BUILD_LOG, len, buffer, nullptr);
		//TODO: make log optional
		std::cout << std::string(buffer);

		throw std::exception("Build error");
	}

	validate_error(err).or_throw(error_message::about_create_program);
	if (!program)
		throw std::exception("Error: Failed to create compute program");

	return program;
}
