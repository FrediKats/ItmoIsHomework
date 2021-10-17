#define CL_USE_DEPRECATED_OPENCL_1_2_APIS

#include <iostream>
#include <CL/opencl.h>
#include "device_provider.h"
#include "execution_context.h"
#include "kernel_file_source.h"
#include "matrix_io.h"
#include "multiplication_kernel.h"
#include "multiplication_kernel_argument.h"
#include "multiplication_kernel_response.h"
#include "sum_kernel.h"
#include "sum_kernel_argument.h"

void execute_sum(int requested_index)
{
    size_t* local = new size_t[]{ 1 };
    size_t* global = new size_t[] { 1 };
    const ocl1::kernel_dimension_config dimension_config = ocl1::kernel_dimension_config(1, local, global);
    sum_kernel_argument argument = sum_kernel_argument(1, 2);
    sum_kernel_response response = sum_kernel_response();

    const ocl1::device device = ocl1::device_provider().select_device(requested_index);
    const ocl1::execution_context execution_context_instance = ocl1::execution_context(device, dimension_config);
    const ocl1::program_builder builder = ocl1::program_builder(execution_context_instance);

    ocl1::kernel_file_source kernel_source = ocl1::kernel_file_source("sum.txt");
    cl_kernel kernel = kernel_source.get_kernel_source_code("sum", builder);

    sum_kernel sum = sum_kernel(execution_context_instance, kernel);
    sum.execute(argument, response);

    std::cout << response.c;
}

void execute_mult(int requested_index, std::string input_path, std::string output_path)
{
    matrix_io matrix_io_instance = matrix_io();
    matrix_multiplication_context multiplication_context = matrix_io_instance.parse_file(input_path);
    const ocl1::kernel_dimension_config dimension_config = multiplication_context.create_config();
    multiplication_kernel_argument argument = multiplication_kernel_argument(multiplication_context);
    multiplication_kernel_response response = multiplication_kernel_response(multiplication_context);

    const ocl1::device device = ocl1::device_provider().select_device(requested_index);
    const ocl1::execution_context execution_context_instance = ocl1::execution_context(device, dimension_config);
    const ocl1::program_builder builder = ocl1::program_builder(execution_context_instance);

    ocl1::kernel_file_source kernel_source = ocl1::kernel_file_source("multiplication.txt");
    cl_kernel kernel = kernel_source.get_kernel_source_code("simpleMultiply", builder);

    multiplication_kernel mult_kernel = multiplication_kernel(execution_context_instance, kernel);
    mult_kernel.execute(argument, response);

    matrix result_matrix(response.result, multiplication_context.second_matrix_width, multiplication_context.first_matrix_height);
    matrix_io_instance.write_matrix(result_matrix, output_path);
}

int main()
{
    //TODO: replace alloc with float[]
    //TODO: release memory object
    //TODO: read index from args
    execute_mult(0, "input_matrix.txt", "output.txt");
    //execute_sum(0);

	return 0;
}
