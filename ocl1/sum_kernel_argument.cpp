#include "sum_kernel_argument.h"

#include "error_message.h"
#include "error_validator.h"

sum_kernel_argument::sum_kernel_argument(int a, int b): a(a), b(b)
{
}

void sum_kernel_argument::write_arguments(execution_context execution_context_instance, cl_kernel kernel)
{
	int err;
	// NB: -37 - invalid host ptr
	cl_mem input_a = clCreateBuffer(execution_context_instance.context, CL_MEM_READ_ONLY, sizeof(int), nullptr, &err);
	validate_error(err).or_throw(error_message::about_kernel_argument_create_buffer);

	cl_mem input_b = clCreateBuffer(execution_context_instance.context, CL_MEM_READ_ONLY, sizeof(int), nullptr, &err);
	validate_error(err).or_throw(error_message::about_kernel_argument_create_buffer);

	err = clEnqueueWriteBuffer(execution_context_instance.command_queue, input_a, CL_TRUE, 0, sizeof(int), &a, 0, nullptr, nullptr);
	validate_error(err).or_throw(error_message::about_kernel_argument_write);
	err = clEnqueueWriteBuffer(execution_context_instance.command_queue, input_b, CL_TRUE, 0, sizeof(int), &b, 0, nullptr, nullptr);
	validate_error(err).or_throw(error_message::about_kernel_argument_write);

	err = clSetKernelArg(kernel, 0, sizeof(cl_mem), &input_a);
	validate_error(err).or_throw(error_message::about_set_kernel_argument);
	err = clSetKernelArg(kernel, 1, sizeof(cl_mem), &input_b);
	validate_error(err).or_throw(error_message::about_set_kernel_argument);
}
