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

void execute_sum()
{
    cl_device_provider device_provider = cl_device_provider();
    //TODO: read index from args
    device device = device_provider.select_device(0);
    execution_context execution_context_instance = execution_context(device);

    kernel_file_source kernel_source = kernel_file_source("sum.txt");
    sum_kernel_argument argument = sum_kernel_argument(1, 2);
    sum_kernel sum = create_sum_kernel(kernel_source, execution_context_instance);
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

void execute_mult()
{
    cl_device_provider device_provider = cl_device_provider();
    //TODO: read index from args
    device device = device_provider.select_device(0);
    execution_context execution_context_instance = execution_context(device);

    kernel_file_source kernel_source = kernel_file_source("multiplication.txt");

    float* a = static_cast<float*>(malloc(sizeof(float)));
    float* b = static_cast<float*>(malloc(sizeof(float)));
    a[0] = 2;
    b[0] = 3;
    float* c = static_cast<float*>(malloc(sizeof(float)));

    multiplication_kernel_argument argument = multiplication_kernel_argument(1, 1, 1, a, b);
    multiplication_kernel_response response = multiplication_kernel_response(1, c);
    multiplication_kernel kernel = create_multiplication_kernel(kernel_source, execution_context_instance);
    kernel.execute(argument, response);
    std::cout << c[0];
    return;
}

int main()
{
    execute_mult();

	return 0;
}
