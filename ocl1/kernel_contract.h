#pragma once

#include <CL/cl.h>

#include "execution_context.h"

template <typename TArguments, typename TResponse>
class kernel_contract
{
public:
	kernel_contract(const execution_context& execution_context_instance, cl_kernel kernel);

	TResponse execute(TArguments argument, TResponse& response);

protected:
	execution_context execution_context_instance_;
	cl_kernel kernel_;
};

template <typename TArguments, typename TResponse>
kernel_contract<TArguments, TResponse>::kernel_contract(
	const execution_context& execution_context_instance,
	cl_kernel kernel)
	: execution_context_instance_(execution_context_instance),
	  kernel_(kernel)
{
}

template <typename TArguments, typename TResponse>
TResponse kernel_contract<TArguments, TResponse>::execute(TArguments argument, TResponse& response)
{
	argument.write_arguments(execution_context_instance_, kernel_);

	//TODO: error handling
	response.setup(execution_context_instance_, kernel_);

	size_t global_item_size = 4;
	size_t local_item_size = 4;
	int err;
	err = clEnqueueNDRangeKernel(execution_context_instance_.command_queue, kernel_, 1, nullptr, &global_item_size,
	                             &local_item_size, 0, nullptr, nullptr);
	clFinish(execution_context_instance_.command_queue);

	response.read_result(execution_context_instance_);

	return response;
}
