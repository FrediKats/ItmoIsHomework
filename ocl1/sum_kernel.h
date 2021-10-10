﻿#pragma once
#include "error_message.h"
#include "error_validator.h"
#include "kernel_contract.h"
#include "kernel_source.h"
#include "program_builder.h"
#include "sum_kernel_argument.h"
#include "sum_kernel_response.h"

class sum_kernel : public kernel_contract<sum_kernel_argument, sum_kernel_response>
{
public:
    explicit sum_kernel(const execution_context& execution_context_instance, cl_kernel kernel);
};

inline sum_kernel create_sum_kernel(kernel_source& source, const execution_context execution_context_instance)
{
    const std::string kernel_source_code = source.get_kernel_source_code();
    program_builder builder = program_builder();
    const cl_program program = builder.build(execution_context_instance.context, kernel_source_code);

    int err;
    cl_kernel kernel = clCreateKernel(program, "sum", &err);
    validate_error(err).or_throw(error_message::about_create_kernel);

    return sum_kernel(execution_context_instance, kernel);
}