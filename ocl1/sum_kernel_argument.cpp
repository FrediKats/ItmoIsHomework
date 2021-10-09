#include "sum_kernel_argument.h"

sum_kernel_argument::sum_kernel_argument(int a, int b): a(a), b(b)
{
}

void sum_kernel_argument::write_arguments(execution_context execution_context_instance, cl_kernel kernel)
{
	int local_a = a;
	int local_b = b;
	int err;
	//TODO: error handling
	// -37 - invalid host ptr
	cl_mem input_a = clCreateBuffer(execution_context_instance.context, CL_MEM_READ_ONLY, sizeof(int), nullptr, &err);
	cl_mem input_b = clCreateBuffer(execution_context_instance.context, CL_MEM_READ_ONLY, sizeof(int), nullptr, &err);

	err = clEnqueueWriteBuffer(execution_context_instance.command_queue, input_a, CL_TRUE, 0, sizeof(int), &local_a, 0, nullptr, nullptr);
	err = clEnqueueWriteBuffer(execution_context_instance.command_queue, input_b, CL_TRUE, 0, sizeof(int), &local_b, 0, nullptr, nullptr);

	clSetKernelArg(kernel, 0, sizeof(cl_mem), &input_a);
	clSetKernelArg(kernel, 1, sizeof(cl_mem), &input_b);
}
