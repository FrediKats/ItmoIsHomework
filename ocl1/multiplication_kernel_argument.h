#pragma once
#include "kernel_argument.h"
#include "matrix_multiplication_context.h"

class multiplication_kernel_argument final : kernel_argument
{
public:
	multiplication_kernel_argument(int n, int k, int m, float* a, float* b);
	explicit multiplication_kernel_argument(const matrix_multiplication_context& multiplication_context);

	void write_arguments(execution_context execution_context_instance, cl_kernel kernel) override;

private:
	size_t n_;
	size_t k_;
	size_t m_;
	float* matrix_a_;
	float* matrix_b_;

	cl_mem matrix_a_input_;
	cl_mem matrix_b_input_;
};
