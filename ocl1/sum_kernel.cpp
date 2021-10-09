#include "sum_kernel.h"

#include "program_builder.h"

sum_kernel::sum_kernel(kernel_source& source, execution_context execution_context_instance): execution_context_instance_(execution_context_instance)
{
    //TODO: error handling
    int err;

    
    const std::string kernel_source_code = source.get_kernel_source_code();
    program_builder builder = program_builder();
    const cl_program program = builder.build(execution_context_instance.context, kernel_source_code);
    cl_kernel kernel = clCreateKernel(program, "sum", &err);

    kernel_ = kernel;
}

sum_kernel_response sum_kernel::execute(sum_kernel_argument argument)
{
    argument.write_arguments(execution_context_instance_, kernel_);

    cl_context context = execution_context_instance_.context;
    cl_command_queue command_queue = execution_context_instance_.command_queue;

    //TODO: error handling
    int err;
    cl_mem output = clCreateBuffer(context, CL_MEM_WRITE_ONLY, sizeof(int), nullptr, &err);


    clSetKernelArg(kernel_, 2, sizeof(cl_mem), &output);

    int c;
    size_t global_item_size = 4;
    size_t local_item_size = 4;
    err = clEnqueueNDRangeKernel(command_queue, kernel_, 1, nullptr, &global_item_size, &local_item_size, 0, nullptr, nullptr);
    clFinish(command_queue);
    err = clEnqueueReadBuffer(command_queue, output, CL_TRUE, 0, sizeof(int), &c, 0, nullptr, nullptr);

    return sum_kernel_response(c);
}
