#pragma once
#include "kernel_response.h"
#include "matrix_multiplication_context.h"

class multiplication_kernel_response final : kernel_response
{
public:
	size_t size;
	float* result;

	explicit multiplication_kernel_response(matrix_multiplication_context context);

	void setup(execution_context execution_context_instance, cl_kernel kernel) override;
	void read_result(execution_context execution_context_instance) override;

private:
	cl_mem output_memory_;
};
