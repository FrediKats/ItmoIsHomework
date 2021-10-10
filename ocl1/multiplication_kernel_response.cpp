#include "multiplication_kernel_response.h"

multiplication_kernel_response::multiplication_kernel_response(size_t size, float* result): size(size), result(result)
{
}

void multiplication_kernel_response::setup(execution_context execution_context_instance, cl_kernel kernel)
{
	//TODO: error handling
	int err;
	cl_mem output = clCreateBuffer(execution_context_instance.context, CL_MEM_WRITE_ONLY, size, nullptr, &err);
	clSetKernelArg(kernel, 4, sizeof(cl_mem), &output);
	output_memory_ = output;
}

void multiplication_kernel_response::read_result(execution_context execution_context_instance)
{
	//TODO: error handling
	int err;
	err = clEnqueueReadBuffer(execution_context_instance.command_queue, output_memory_, CL_TRUE, 0, size, result, 0, nullptr, nullptr);
}
