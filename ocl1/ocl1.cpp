#define CL_USE_DEPRECATED_OPENCL_1_2_APIS
#define CL_TARGET_OPENCL_VERSION 120

#include <iostream>
#include <CL/opencl.h>
#include "device_provider.h"
#include "execution_context.h"
#include "kernel_file_source.h"
#include "matrix_io.h"
#include "matrix_size_changer.h"
#include "multiplication_kernel.h"
#include "multiplication_kernel_argument.h"
#include "multiplication_kernel_response.h"
#include "sum_kernel.h"
#include "sum_kernel_argument.h"

size_t bound(size_t value)
{
    int TS = 32;
    return value / TS * TS + (value % TS != 0) * TS;
}

size_t max_bound(size_t a, size_t b, size_t c)
{
    return bound(std::max(a, std::max(b, c)));
}

size_t max_bound(matrix_multiplication_context context)
{
    return max_bound(context.first_matrix_height, context.k, context.second_matrix_width);
}


void execute_sum(int requested_index)
{
    size_t* local = new size_t[]{ 1 };
    size_t* global = new size_t[] { 1 };
    const ocl1::kernel_dimension_config dimension_config = ocl1::kernel_dimension_config(1, local, global);
    sum_kernel_argument argument = sum_kernel_argument(1, 2);
    sum_kernel_response response = sum_kernel_response();

    const bool trace_detailed_info = false;
    const ocl1::device device = ocl1::device_provider().select_device(requested_index, trace_detailed_info);
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

    bool const trace_detailed_info = false;
    const ocl1::device device = ocl1::device_provider().select_device(requested_index, trace_detailed_info);
    const ocl1::execution_context execution_context_instance = ocl1::execution_context(device, dimension_config);
    const ocl1::program_builder builder = ocl1::program_builder(execution_context_instance);

    ocl1::kernel_file_source kernel_source = ocl1::kernel_file_source("multiplication.txt");
    cl_kernel kernel = kernel_source.get_kernel_source_code("simpleMultiply", builder);

    multiplication_kernel mult_kernel = multiplication_kernel(execution_context_instance, kernel);
    mult_kernel.execute(argument, response);

    matrix result_matrix(response.result, multiplication_context.second_matrix_width, multiplication_context.first_matrix_height);
    matrix_io_instance.write_matrix(result_matrix, output_path);
}

void execute_mult_with_local(int requested_index, std::string input_path, std::string output_path)
{
    matrix_io matrix_io_instance = matrix_io();
    matrix_multiplication_context multiplication_context = matrix_io_instance.parse_file(input_path);
	auto changer = matrix_size_changer(multiplication_context, max_bound(multiplication_context));

	const ocl1::kernel_dimension_config dimension_config = changer.modified_context_.create_config_with_local();
    multiplication_kernel_argument argument = multiplication_kernel_argument(changer.modified_context_);
    multiplication_kernel_response response = multiplication_kernel_response(changer.modified_context_);

    bool const trace_detailed_info = false;
    const ocl1::device device = ocl1::device_provider().select_device(requested_index, trace_detailed_info);
    const ocl1::execution_context execution_context_instance = ocl1::execution_context(device, dimension_config);
    const ocl1::program_builder builder = ocl1::program_builder(execution_context_instance);

    ocl1::kernel_file_source kernel_source = ocl1::kernel_file_source("OptimaizedMultiplication.txt");
    cl_kernel kernel = kernel_source.get_kernel_source_code("multi_with_local_memory", builder);

    multiplication_kernel mult_kernel = multiplication_kernel(execution_context_instance, kernel);
    mult_kernel.execute(argument, response);

    matrix result_matrix(response.result, changer.modified_context_.first_matrix_height, changer.modified_context_.second_matrix_width);
    result_matrix = result_matrix.resize(changer.original_context_.first_matrix_height, changer.original_context_.second_matrix_width);
    matrix_io_instance.write_matrix(result_matrix, output_path);
}

int cli_execute(int argc, char** argv)
{
    try
    {
        if (argc != 5)
        {
            std::cerr << "Unexpected argument count";
            return 1;
        }

        int device_index = atoi(argv[1]);
        std::string input_path(argv[2]);
        std::string output_path(argv[3]);
        int algo_type = atoi(argv[4]);

        switch (algo_type)
        {
        case 1:
            execute_mult(device_index, input_path, output_path);
            return 0;

        case 2:
            execute_mult_with_local(device_index, input_path, output_path);
            return 0;

        default:
	        std::cerr << "Not supported algo type.";
            return 1;
        }
    }
    catch (std::exception& e)
    {
        std::cerr << "Unexpected error.\n" << e.what();
        return 1;
    }
    catch (...)
    {
        std::cerr << "Unexpected error.";
        return 1;
    }
}

int main(int argc, char** argv)
{
    return cli_execute(argc, argv);

    //TODO: replace alloc with float[]
    //TODO: release memory object
    //TODO: read index from args
    //execute_mult(0, "input_matrix.txt", "output.txt");
    execute_mult_with_local(0, "input_matrix.txt", "output.txt");
    //execute_sum(0);

	return 0;
}
