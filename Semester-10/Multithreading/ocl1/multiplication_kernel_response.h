﻿#pragma once
#include "kernel_response.h"
#include "matrix_multiplication_context.h"

class multiplication_kernel_response final : ocl1::kernel_response
{
public:
	size_t size;
	float* result;

	explicit multiplication_kernel_response(matrix_multiplication_context context);

	void setup(ocl1::execution_context execution_context_instance, cl_kernel kernel) override;
	void read_result(ocl1::execution_context execution_context_instance) override;

	~multiplication_kernel_response() override;

private:
	cl_mem output_memory_;
};
