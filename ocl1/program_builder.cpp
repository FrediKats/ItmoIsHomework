#include "program_builder.h"

#include <iostream>

#include "error_message.h"
#include "error_validator.h"

namespace ocl1
{
	program_builder::program_builder(cl_context context, const device& device): context_(context), device_(device)
	{
	}

	program_builder::program_builder(execution_context context) : context_(context.context), device_(context.selected_device)
	{
	}

	cl_program program_builder::build(const std::string& kernel_source_code)
	{
		const char* kernel_source_code_link = kernel_source_code.c_str();
		int err;

		//TODO: can optimize this logic wit prebuild
		cl_program program = clCreateProgramWithSource(context_, 1, &kernel_source_code_link, nullptr, &err);
		validate_error(err).or_throw(error_message::about_create_program);
		if (!program)
			throw std::exception("Error: Failed to create compute program");

		cl_device_id device_id = device_.id;
		err = clBuildProgram(program, 1, &device_id, nullptr, nullptr, nullptr);
		if (err != CL_SUCCESS)
		{
			size_t len = 0;
			cl_int ret = CL_SUCCESS;
			ret = clGetProgramBuildInfo(program, nullptr, CL_PROGRAM_BUILD_LOG, 0, nullptr, &len);
			char* buffer = static_cast<char*>(calloc(len, sizeof(char)));
			ret = clGetProgramBuildInfo(program, device_.id, CL_PROGRAM_BUILD_LOG, len, buffer, nullptr);
			//TODO: make log optionalf
			std::cout << std::string(buffer);

			throw std::exception("Build error");
		}

		validate_error(err).or_throw(error_message::about_create_program);
		if (!program)
			throw std::exception("Error: Failed to create compute program");

		return program;
	}
}
