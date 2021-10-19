#include "multiplication_kernel_response.h"

#include "error_message.h"
#include "error_validator.h"

multiplication_kernel_response::multiplication_kernel_response(matrix_multiplication_context context)
	: size(context.second_matrix_width * context.first_matrix_height),
	  result(new float[size])
{
}

void multiplication_kernel_response::setup(ocl1::execution_context execution_context_instance, cl_kernel kernel)
{
	const auto allocate_size = sizeof(float) * size;

	int err;
	output_memory_ = clCreateBuffer(
		execution_context_instance.context,
		CL_MEM_WRITE_ONLY,
		allocate_size,
		nullptr,
		&err);
	validate_error(err).or_throw(about_kernel_response_setup);

	clSetKernelArg(kernel, 5, sizeof(cl_mem), &output_memory_);
	validate_error(err).or_throw(about_set_kernel_argument);
}

void multiplication_kernel_response::read_result(ocl1::execution_context execution_context_instance)
{
	auto allocate_size = sizeof(float) * size;

	const int err = clEnqueueReadBuffer(
		execution_context_instance.command_queue,
		output_memory_,
		CL_TRUE,
		0,
		allocate_size,
		result,
		0,
		nullptr,
		nullptr);
	validate_error(err).or_throw(about_kernel_response_read);
}

multiplication_kernel_response::~multiplication_kernel_response()
{
	//clReleaseMemObject(output_memory_);
	//clReleaseMemObject(output_memory_);
}
