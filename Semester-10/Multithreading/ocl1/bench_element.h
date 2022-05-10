#pragma once
#include "multiplication_kernel.h"

class bench_element
{
public:
	multiplication_kernel kernel;
	multiplication_kernel_argument argument;
	multiplication_kernel_response response;

	bench_element(
		const multiplication_kernel& kernel,
		const multiplication_kernel_argument& argument,
		const multiplication_kernel_response& response)
		: kernel(kernel),
		  argument(argument),
		  response(response)
	{
	}

	execution_benchmark_result run()
	{
		const execution_benchmark_result benchmark_result = kernel.execute(argument, response);
		return benchmark_result;
	}
};
