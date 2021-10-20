#pragma once
#include "bench_element.h"
#include "kernel_file_source.h"
#include "matrix_extensions.h"
#include "matrix_size_changer.h"
#include "program_builder.h"

class matrix_benchmark
{
public:
	const int run_count = 1;
    ocl1::device selected_device;

private:
	bench_element create_naive_algo(matrix_multiplication_context multiplication_context)
	{
        const ocl1::kernel_dimension_config dimension_config = multiplication_context.create_config();
        multiplication_kernel_argument argument = multiplication_kernel_argument(multiplication_context);
        multiplication_kernel_response response = multiplication_kernel_response(multiplication_context);

        const ocl1::execution_context execution_context_instance = ocl1::execution_context(selected_device, dimension_config);
        const ocl1::program_builder builder = ocl1::program_builder(execution_context_instance);

        ocl1::kernel_file_source kernel_source = ocl1::kernel_file_source("multiplication.txt");
        cl_kernel kernel = kernel_source.get_kernel_source_code("simpleMultiply", builder);

        multiplication_kernel mult_kernel = multiplication_kernel(execution_context_instance, kernel);
        return bench_element(mult_kernel, argument, response);
	}

	bench_element create_local_memory_algo(matrix_multiplication_context multiplication_context)
	{
        auto changer = matrix_size_changer(multiplication_context, max_bound(multiplication_context));

        const ocl1::kernel_dimension_config dimension_config = changer.modified_context_.create_config_with_local();
        multiplication_kernel_argument argument = multiplication_kernel_argument(changer.modified_context_);
        multiplication_kernel_response response = multiplication_kernel_response(changer.modified_context_);

        const ocl1::execution_context execution_context_instance = ocl1::execution_context(selected_device, dimension_config);
        const ocl1::program_builder builder = ocl1::program_builder(execution_context_instance);

        ocl1::kernel_file_source kernel_source = ocl1::kernel_file_source("OptimaizedMultiplication.txt");
        cl_kernel kernel = kernel_source.get_kernel_source_code("multi_with_local_memory", builder);

        multiplication_kernel mult_kernel = multiplication_kernel(execution_context_instance, kernel);
        return bench_element(mult_kernel, argument, response);
	}
};
