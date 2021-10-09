#define CL_USE_DEPRECATED_OPENCL_1_2_APIS

#include <fstream>
#include <iostream>
#include <CL/opencl.h>
#include <vector>
#include "cl_device_provider.h"
#include "execution_context.h"
#include "kernel_file_source.h"
#include "program_builder.h"


int main()
{
    int err;

    kernel_file_source kernel_source = kernel_file_source("kernel.txt");
    const std::string kernel_source_code = kernel_source.get_kernel_source_code();

    cl_device_provider device_provider = cl_device_provider();
    //TODO: read index from args
    device device = device_provider.select_device(0);


    execution_context execution_context_instance = execution_context(device);
    cl_context context = execution_context_instance.context;
    cl_command_queue command_queue = execution_context_instance.command_queue;

    program_builder builder = program_builder();
    const cl_program program = builder.build(context, kernel_source_code);

    int a = 1;
    int b = 2;
    int c = 0;

    // -37 - invalid host ptr
    cl_mem input_a = clCreateBuffer(context, CL_MEM_READ_ONLY, sizeof(int), nullptr, &err);
    cl_mem input_b = clCreateBuffer(context, CL_MEM_READ_ONLY, sizeof(int), nullptr, &err);
    cl_mem output = clCreateBuffer(context, CL_MEM_WRITE_ONLY, sizeof(int), nullptr, &err);

    err = clEnqueueWriteBuffer(command_queue, input_a, CL_TRUE, 0, sizeof(int), &a, 0, nullptr, nullptr);
    err = clEnqueueWriteBuffer(command_queue, input_b, CL_TRUE, 0, sizeof(int), &b, 0, nullptr, nullptr);

    cl_kernel kernel = clCreateKernel(program, "sum", &err);
    clSetKernelArg(kernel, 0, sizeof(cl_mem), &input_a);
    clSetKernelArg(kernel, 1, sizeof(cl_mem), &input_b);
    clSetKernelArg(kernel, 2, sizeof(cl_mem), &output);

    size_t global_item_size = 4;
    size_t local_item_size = 4;
    err = clEnqueueNDRangeKernel(command_queue, kernel, 1, nullptr, &global_item_size, &local_item_size, 0, nullptr, nullptr);
    clFinish(command_queue);
    err = clEnqueueReadBuffer(command_queue, output, CL_TRUE, 0, sizeof(int), &c, 0, nullptr, nullptr);

    //auto buffer = clCreateBuffer(context, 0, sizeof(cl_int), nullptr, nullptr);

    std::cout << c;

    //TODO: release memory object
    //auto buffer = clCreateBuffer(context, 0, sizeof(cl_int), nullptr, nullptr);
    //TODO: add sync | async
    //cl_int write_byte_count = clEnqueueWriteBuffer(command_queue, buffer, false, 0, sizeof(cl_int), nullptr, 0, nullptr, nullptr);
    //cl_int read_byte_count = clEnqueueReadBuffer(command_queue, buffer, false, 0, sizeof(cl_int), nullptr, 0, nullptr, nullptr);

	return 0;
}
