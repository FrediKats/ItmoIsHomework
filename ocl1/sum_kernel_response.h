#pragma once
#include "execution_context.h"
#include "kernel_response.h"

class sum_kernel_response final : public ocl1::kernel_response
{
public:
	int c;

	sum_kernel_response();

	void setup(ocl1::execution_context execution_context_instance, cl_kernel kernel) override;
	void read_result(ocl1::execution_context execution_context_instance) override;

private:
	cl_mem output_memory_;
};
