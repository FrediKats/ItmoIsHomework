#include "sum_kernel_response.h"

#include "error_message.h"
#include "error_validator.h"

sum_kernel_response::sum_kernel_response()
{
}

void sum_kernel_response::setup(execution_context execution_context_instance, cl_kernel kernel)
{
	int err;
	const cl_mem output = clCreateBuffer(execution_context_instance.context, CL_MEM_WRITE_ONLY, sizeof(int), nullptr, &err);

	validate_error(err).or_throw(error_message::about_kernel_response_setup);

	clSetKernelArg(kernel, 2, sizeof(cl_mem), &output);
	output_memory_ = output;
}

void sum_kernel_response::read_result(execution_context execution_context_instance)
{
	int err;
	err = clEnqueueReadBuffer(execution_context_instance.command_queue, output_memory_, CL_TRUE, 0, sizeof(int), &c, 0, nullptr, nullptr);
	validate_error(err).or_throw(error_message::about_kernel_response_read);
}
