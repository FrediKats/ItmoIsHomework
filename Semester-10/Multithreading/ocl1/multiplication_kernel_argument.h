#pragma once
#include "kernel_argument.h"
#include "matrix_multiplication_context.h"

class multiplication_kernel_argument final : ocl1::kernel_argument
{
public:
	multiplication_kernel_argument(int n, int k, int m, float* a, float* b);
	explicit multiplication_kernel_argument(const matrix_multiplication_context& multiplication_context);

	void write_arguments(ocl1::execution_context context, cl_kernel kernel) override;

	~multiplication_kernel_argument() override
	{
		//clReleaseMemObject(matrix_a_input_);
		//clReleaseMemObject(matrix_b_input_);
		//clReleaseMemObject(mem_second_matrix_width_);
		//clReleaseMemObject(mem_k_);
		//clReleaseMemObject(mem_first_matrix_height_);
	}
private:
	size_t second_matrix_width_;
	size_t k_;
	size_t first_matrix_height_;
	float* matrix_a_;
	float* matrix_b_;

	cl_mem matrix_a_input_;
	cl_mem matrix_b_input_;
	cl_mem mem_second_matrix_width_;
	cl_mem mem_k_;
	cl_mem mem_first_matrix_height_;
};
