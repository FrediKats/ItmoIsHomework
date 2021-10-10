#pragma once
#include "kernel_response.h"

class multiplication_kernel_response final : kernel_response
{
public:
	size_t size;
	float* result;

	multiplication_kernel_response(size_t size, float* result);

	void setup(execution_context execution_context_instance, cl_kernel kernel) override;
	void read_result(execution_context execution_context_instance) override;

private:
	cl_mem output_memory_;
};
