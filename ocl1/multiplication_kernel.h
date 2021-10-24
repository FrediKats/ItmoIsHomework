#pragma once

#include "kernel_contract.h"
#include "multiplication_kernel_argument.h"
#include "multiplication_kernel_response.h"

class multiplication_kernel : public ocl1::kernel_contract<multiplication_kernel_argument, multiplication_kernel_response>
{
public:
	multiplication_kernel(const ocl1::execution_context& execution_context_instance, cl_kernel kernel);
};
