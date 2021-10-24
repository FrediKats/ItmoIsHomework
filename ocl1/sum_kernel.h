#pragma once

#include "kernel_contract.h"
#include "sum_kernel_argument.h"
#include "sum_kernel_response.h"

class sum_kernel : public ocl1::kernel_contract<sum_kernel_argument, sum_kernel_response>
{
public:
    explicit sum_kernel(const ocl1::execution_context& execution_context_instance, cl_kernel kernel);
};
