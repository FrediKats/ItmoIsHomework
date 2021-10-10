#include "kernel_file_source.h"

#include <fstream>
#include <utility>

#include "error_message.h"
#include "error_validator.h"
#include "kernel_argument.h"
#include "program_builder.h"

kernel_file_source::kernel_file_source(std::string file_path): file_path_(std::move(file_path))
{
}

//NB: https://stackoverflow.com/a/28344440
cl_kernel kernel_file_source::get_kernel_source_code(
	const execution_context execution_context_instance,
	const std::string kernel_name,
	program_builder builder)
{
	std::ifstream ifs(file_path_);
	try
	{
		auto result = std::string((std::istreambuf_iterator<char>(ifs)), (std::istreambuf_iterator<char>()));
		const cl_program program = builder.build(execution_context_instance.context, result, execution_context_instance.selected_device);

		int err;
		cl_kernel kernel = clCreateKernel(program, kernel_name.c_str(), &err);
		validate_error(err).or_throw(error_message::about_create_kernel);

		ifs.close();
		return kernel;
	}
	catch (...)
	{
		ifs.close();
		throw;
	}
}
