#include "kernel_file_source.h"

#include <fstream>
#include <utility>

#include "error_message.h"
#include "error_validator.h"
#include "program_builder.h"

namespace ocl1
{
	kernel_file_source::kernel_file_source(std::string file_path): file_path_(std::move(file_path))
	{
	}

	//NB: https://stackoverflow.com/a/28344440
	//NB: Do not call .close because of https://stackoverflow.com/a/748059
	cl_kernel kernel_file_source::get_kernel_source_code(const std::string kernel_name, program_builder builder)
	{
		std::ifstream ifs(file_path_);
		const auto result = std::string(std::istreambuf_iterator<char>(ifs), std::istreambuf_iterator<char>());
		//TODO: clean program
		cl_program program = builder.build(result);

		int err;
		cl_kernel kernel = clCreateKernel(program, kernel_name.c_str(), &err);
		validate_error(err).or_throw(error_message::about_create_kernel);

		return kernel;
	}
}
