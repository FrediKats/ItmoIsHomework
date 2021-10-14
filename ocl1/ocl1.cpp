#define CL_USE_DEPRECATED_OPENCL_1_2_APIS

#include <iostream>
#include <CL/opencl.h>
#include "cl_device_provider.h"
#include "execution_context.h"
#include "kernel_file_source.h"
#include "multiplication_kernel.h"
#include "multiplication_kernel_argument.h"
#include "multiplication_kernel_response.h"
#include "sum_kernel.h"
#include "sum_kernel_argument.h"

void execute_sum(int requested_index)
{
    size_t* local = new size_t[]{ 1 };
    size_t* global = new size_t[] { 1 };
    const kernel_dimension_config dimension_config = kernel_dimension_config(1, local, global);

    cl_device_provider device_provider = cl_device_provider();
    device device = device_provider.select_device(requested_index);
    execution_context execution_context_instance = execution_context(device, dimension_config);

    program_builder builder = program_builder();
    kernel_file_source kernel_source = kernel_file_source("sum.txt");
    cl_kernel kernel = kernel_source.get_kernel_source_code(execution_context_instance, "sum", builder);

    sum_kernel sum = sum_kernel(execution_context_instance, kernel);
    sum_kernel_argument argument = sum_kernel_argument(1, 2);
    sum_kernel_response response = sum_kernel_response();

    sum.execute(argument, response);

    std::cout << response.c;

    //auto buffer = clCreateBuffer(context, 0, sizeof(cl_int), nullptr, nullptr);

    //TODO: release memory object
    //auto buffer = clCreateBuffer(context, 0, sizeof(cl_int), nullptr, nullptr);
    //TODO: add sync | async
    //cl_int write_byte_count = clEnqueueWriteBuffer(command_queue, buffer, false, 0, sizeof(cl_int), nullptr, 0, nullptr, nullptr);
    //cl_int read_byte_count = clEnqueueReadBuffer(command_queue, buffer, false, 0, sizeof(cl_int), nullptr, 0, nullptr, nullptr);
}

void execute_mult(int requested_index)
{
    const kernel_dimension_config dimension_config = kernel_dimension_config(2, new size_t[]{ 32, 32 }, new size_t[]{ 1, 1 });
    matrix_multiplication_context multiplication_context = matrix_multiplication_context(1, 1, 1, matrix(std::vector<std::vector<float>>{ {1}}), matrix(std::vector<std::vector<float>>{ {1}}));

    cl_device_provider device_provider = cl_device_provider();
    device device = device_provider.select_device(requested_index);
    execution_context execution_context_instance = execution_context(device, dimension_config);

    program_builder builder = program_builder();
    kernel_file_source kernel_source = kernel_file_source("multiplication.txt");
    cl_kernel kernel = kernel_source.get_kernel_source_code(execution_context_instance, "simpleMultiply", builder);

    float* c = static_cast<float*>(calloc(1, sizeof(float)));
    c[0] = -1;

    multiplication_kernel mult_kernel = multiplication_kernel(execution_context_instance, kernel);
    multiplication_kernel_argument argument = multiplication_kernel_argument(multiplication_context);
    multiplication_kernel_response response = multiplication_kernel_response(1, c);

    mult_kernel.execute(argument, response);

    std::cout << c[0];
}

int main()
{
    //TODO: read index from args
    execute_mult(0);
    //execute_sum(0);

	return 0;
}
