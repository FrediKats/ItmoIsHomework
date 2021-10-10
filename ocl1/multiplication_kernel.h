#pragma once
#include "error_validator.h"
#include "kernel_contract.h"
#include "kernel_source.h"
#include "multiplication_kernel_argument.h"
#include "multiplication_kernel_response.h"
#include "program_builder.h"

class multiplication_kernel : public kernel_contract<multiplication_kernel_argument, multiplication_kernel_response>
{
public:
	multiplication_kernel(const execution_context& execution_context_instance, cl_kernel kernel)
		: kernel_contract<multiplication_kernel_argument, multiplication_kernel_response>(
			execution_context_instance, kernel)
	{
	}
};


inline multiplication_kernel create_multiplication_kernel(kernel_source& source, const execution_context execution_context_instance)
{
    const std::string kernel_source_code = source.get_kernel_source_code();
    program_builder builder = program_builder();
    const cl_program program = builder.build(execution_context_instance.context, kernel_source_code, execution_context_instance.selected_device);

    int err;
    cl_kernel kernel = clCreateKernel(program, "simpleMultiply", &err);
    validate_error(err).or_throw(error_message::about_create_kernel);

    return multiplication_kernel(execution_context_instance, kernel);
}