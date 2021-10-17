#include "multiplication_kernel_argument.h"

#include "error_message.h"
#include "error_validator.h"

multiplication_kernel_argument::multiplication_kernel_argument(int n, int k, int m, float* a, float* b):
	second_matrix_width_(n),
	k_(k),
	first_matrix_height_(m),
	matrix_a_(a),
	matrix_b_(b)
{
}

multiplication_kernel_argument::multiplication_kernel_argument(
	const matrix_multiplication_context& multiplication_context):
	second_matrix_width_(multiplication_context.second_matrix_width),
	k_(multiplication_context.k),
	first_matrix_height_(multiplication_context.first_matrix_height),
	matrix_a_(multiplication_context.first.to_array()),
	matrix_b_(multiplication_context.second.to_array())
{
}

void multiplication_kernel_argument::write_arguments(ocl1::execution_context execution_context_instance,
                                                     cl_kernel kernel)
{
	unsigned long long matrix_a_size = sizeof(float) * k_ * first_matrix_height_;
	unsigned long long matrix_b_size = sizeof(float) * second_matrix_width_ * k_;

	size_t width_a = k_;
	size_t width_b = second_matrix_width_;

	int err;
	matrix_a_input_ = clCreateBuffer(execution_context_instance.context, CL_MEM_READ_ONLY, matrix_a_size, nullptr,
	                                 &err);
	validate_error(err).or_throw(about_kernel_argument_create_buffer);
	err = clEnqueueWriteBuffer(execution_context_instance.command_queue, matrix_a_input_, CL_TRUE, 0, matrix_a_size,
	                           matrix_a_, 0, nullptr, nullptr);
	validate_error(err).or_throw(about_kernel_argument_write);
	err = clSetKernelArg(kernel, 0, sizeof(cl_mem), &matrix_a_input_);

	//NB: CL_INVALID_ARG_SIZE -51
	validate_error(err).or_throw(about_set_kernel_argument);

	matrix_b_input_ = clCreateBuffer(execution_context_instance.context, CL_MEM_READ_ONLY, matrix_b_size, nullptr,
	                                 &err);
	validate_error(err).or_throw(about_kernel_argument_create_buffer);
	err = clEnqueueWriteBuffer(execution_context_instance.command_queue, matrix_b_input_, CL_TRUE, 0, matrix_b_size,
	                           matrix_b_, 0, nullptr, nullptr);
	validate_error(err).or_throw(about_kernel_argument_write);
	err = clSetKernelArg(kernel, 1, sizeof(cl_mem), &matrix_b_input_);
	validate_error(err).or_throw(about_set_kernel_argument);

	cl_mem width_a_input = clCreateBuffer(execution_context_instance.context, CL_MEM_READ_ONLY, sizeof(int), nullptr,
	                                      &err);
	validate_error(err).or_throw(about_kernel_argument_create_buffer);
	err = clEnqueueWriteBuffer(execution_context_instance.command_queue, width_a_input, CL_TRUE, 0, sizeof(int),
	                           &width_a, 0, nullptr, nullptr);
	validate_error(err).or_throw(about_kernel_argument_write);
	err = clSetKernelArg(kernel, 2, sizeof(cl_mem), &width_a_input);
	validate_error(err).or_throw(about_set_kernel_argument);

	cl_mem width_b_input = clCreateBuffer(execution_context_instance.context, CL_MEM_READ_ONLY, sizeof(int), nullptr,
	                                      &err);
	validate_error(err).or_throw(about_kernel_argument_create_buffer);
	err = clEnqueueWriteBuffer(execution_context_instance.command_queue, width_b_input, CL_TRUE, 0, sizeof(int),
	                           &width_b, 0, nullptr, nullptr);
	validate_error(err).or_throw(about_kernel_argument_write);
	err = clSetKernelArg(kernel, 3, sizeof(cl_mem), &width_b_input);
	validate_error(err).or_throw(about_set_kernel_argument);
}
