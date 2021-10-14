﻿#pragma once
#include "kernel_argument.h"

class multiplication_kernel_argument final : kernel_argument
{
public:
	multiplication_kernel_argument(int n, int k, int m, float* a, float* b);

	void write_arguments(execution_context execution_context_instance, cl_kernel kernel) override;

private:
	int n_;
	int k_;
	int m_;
	float* matrix_a_;
	float* matrix_b_;

	cl_mem matrix_a_input_;
	cl_mem matrix_b_input_;
};
