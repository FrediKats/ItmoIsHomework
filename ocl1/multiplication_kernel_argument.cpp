#include "multiplication_kernel_argument.h"

#include "error_message.h"
#include "error_validator.h"

multiplication_kernel_argument::multiplication_kernel_argument(int n, int k, int m, float* a, float* b): n_(n),
                                                                                                         k_(k),
                                                                                                         m_(m),
                                                                                                         matrix_a_(a),
                                                                                                         matrix_b_(b)
{
}

void multiplication_kernel_argument::write_arguments(execution_context execution_context_instance, cl_kernel kernel)
{
	unsigned long long matrix_a_size = sizeof(float) * k_ * m_;
	unsigned long long matrix_b_size = sizeof(float) * n_ * k_;

	int width_a = k_;
	int width_b = n_;

	int err;
	cl_mem matrix_a_input = clCreateBuffer(execution_context_instance.context, CL_MEM_READ_ONLY, matrix_a_size, nullptr, &err);
	validate_error(err).or_throw(error_message::about_kernel_argument_create_buffer);
	err = clEnqueueWriteBuffer(execution_context_instance.command_queue, matrix_a_input, CL_TRUE, 0, matrix_a_size, &matrix_a_, 0, nullptr, nullptr);
	validate_error(err).or_throw(error_message::about_kernel_argument_write);
	err = clSetKernelArg(kernel, 0, sizeof(cl_mem), &matrix_a_input);

	//NB: CL_INVALID_ARG_SIZE -51
	validate_error(err).or_throw(error_message::about_set_kernel_argument);

	cl_mem matrix_b_input = clCreateBuffer(execution_context_instance.context, CL_MEM_READ_ONLY, matrix_b_size, nullptr, &err);
	validate_error(err).or_throw(error_message::about_kernel_argument_create_buffer);
	err = clEnqueueWriteBuffer(execution_context_instance.command_queue, matrix_b_input, CL_TRUE, 0, matrix_b_size, &matrix_b_, 0, nullptr, nullptr);
	validate_error(err).or_throw(error_message::about_kernel_argument_write);
	err = clSetKernelArg(kernel, 1, sizeof(cl_mem), &matrix_b_input);
	validate_error(err).or_throw(error_message::about_set_kernel_argument);

	cl_mem width_a_input = clCreateBuffer(execution_context_instance.context, CL_MEM_READ_ONLY, sizeof(int), nullptr, &err);
	validate_error(err).or_throw(error_message::about_kernel_argument_create_buffer);
	err = clEnqueueWriteBuffer(execution_context_instance.command_queue, width_a_input, CL_TRUE, 0, sizeof(int), &width_a, 0, nullptr, nullptr);
	validate_error(err).or_throw(error_message::about_kernel_argument_write);
	err = clSetKernelArg(kernel, 2, sizeof(cl_mem), &width_a_input);
	validate_error(err).or_throw(error_message::about_set_kernel_argument);

	cl_mem width_b_input = clCreateBuffer(execution_context_instance.context, CL_MEM_READ_ONLY, sizeof(int), nullptr, &err);
	validate_error(err).or_throw(error_message::about_kernel_argument_create_buffer);
	err = clEnqueueWriteBuffer(execution_context_instance.command_queue, width_b_input, CL_TRUE, 0, sizeof(int), &width_b, 0, nullptr, nullptr);
	validate_error(err).or_throw(error_message::about_kernel_argument_write);
	err = clSetKernelArg(kernel, 3, sizeof(cl_mem), &width_b_input);
	validate_error(err).or_throw(error_message::about_set_kernel_argument);
}
