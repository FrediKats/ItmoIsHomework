#include "multiplication_kernel_argument.h"

multiplication_kernel_argument::multiplication_kernel_argument(int n, int k, int m, float* a, float* b): n_(n),
	k_(k),
	m_(m),
	matrix_a_(a),
	matrix_b_(b)
{
}

void multiplication_kernel_argument::write_arguments(execution_context execution_context_instance, cl_kernel kernel)
{
	int matrix_a_size = k_ * m_;
	int matrix_b_size = n_ * k_;

	int width_a = k_;
	int width_b = n_;

	int err;
	//TODO: error handling
	cl_mem matrix_a_input = clCreateBuffer(execution_context_instance.context, CL_MEM_READ_ONLY, sizeof(int) * matrix_a_size, nullptr, &err);
	cl_mem matrix_b_input = clCreateBuffer(execution_context_instance.context, CL_MEM_READ_ONLY, sizeof(int) * matrix_b_size, nullptr, &err);
	cl_mem width_a_input = clCreateBuffer(execution_context_instance.context, CL_MEM_READ_ONLY, sizeof(int), nullptr, &err);
	cl_mem width_b_input = clCreateBuffer(execution_context_instance.context, CL_MEM_READ_ONLY, sizeof(int), nullptr, &err);

	err = clEnqueueWriteBuffer(execution_context_instance.command_queue, matrix_a_input, CL_TRUE, 0, sizeof(int) * matrix_a_size, &matrix_a_, 0, nullptr, nullptr);
	err = clEnqueueWriteBuffer(execution_context_instance.command_queue, matrix_b_input, CL_TRUE, 0, sizeof(int) * matrix_b_size, &matrix_b_, 0, nullptr, nullptr);
	err = clEnqueueWriteBuffer(execution_context_instance.command_queue, width_a_input, CL_TRUE, 0, sizeof(int), &width_a, 0, nullptr, nullptr);
	err = clEnqueueWriteBuffer(execution_context_instance.command_queue, width_b_input, CL_TRUE, 0, sizeof(int), &width_b, 0, nullptr, nullptr);

	clSetKernelArg(kernel, 0, sizeof(cl_mem), &matrix_a_input);
	clSetKernelArg(kernel, 1, sizeof(cl_mem), &matrix_b_input);
	clSetKernelArg(kernel, 2, sizeof(cl_mem), &width_a_input);
	clSetKernelArg(kernel, 3, sizeof(cl_mem), &width_b_input);
}
