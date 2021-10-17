#include "multiplication_kernel_response.h"

#include "error_message.h"
#include "error_validator.h"

//TODO: add column | row
multiplication_kernel_response::multiplication_kernel_response(matrix_multiplication_context context)
	: size(context.n * context.m),
	  result(new float[size])
{
}

void multiplication_kernel_response::setup(execution_context execution_context_instance, cl_kernel kernel)
{
	auto allocate_size = sizeof(float) * size;

	int err;
	cl_mem output = clCreateBuffer(execution_context_instance.context, CL_MEM_WRITE_ONLY, allocate_size, nullptr, &err);
	validate_error(err).or_throw(about_kernel_response_setup);

	clSetKernelArg(kernel, 4, sizeof(cl_mem), &output);
	validate_error(err).or_throw(about_set_kernel_argument);
	output_memory_ = output;
}

void multiplication_kernel_response::read_result(execution_context execution_context_instance)
{
	auto allocate_size = sizeof(float) * size;

	int err;
	err = clEnqueueReadBuffer(execution_context_instance.command_queue, output_memory_, CL_TRUE, 0, allocate_size,
	                          result, 0, nullptr, nullptr);
	validate_error(err).or_throw(about_kernel_response_read);
}
