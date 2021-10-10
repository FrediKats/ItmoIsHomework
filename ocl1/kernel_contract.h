#pragma once

#include <CL/cl.h>

#include "execution_context.h"
#include "error_validator.h"
#include "error_message.h"

template <typename TArguments, typename TResponse>
class kernel_contract
{
public:
	kernel_contract(const execution_context& execution_context_instance, cl_kernel kernel);

	void execute(TArguments& argument, TResponse& response);

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
void kernel_contract<TArguments, TResponse>::execute(TArguments& argument, TResponse& response)
{
	argument.write_arguments(execution_context_instance_, kernel_);

	response.setup(execution_context_instance_, kernel_);

	size_t global_item_size = 1024;
	size_t local_item_size = 1024;

	int err;
	err = clEnqueueNDRangeKernel(
		execution_context_instance_.command_queue,
		kernel_,
		1,
		nullptr,
		&global_item_size,
		&local_item_size,
		0,
		nullptr,
		nullptr);

	validate_error(err).or_throw(error_message::about_kernel_enqueue);

	//TODO: not sure about order
	//err = clFinish(execution_context_instance_.command_queue);
	//validate_error(err).or_throw(error_message::about_waiting_processing);

	response.read_result(execution_context_instance_);
	err = clFinish(execution_context_instance_.command_queue);
	validate_error(err).or_throw(error_message::about_waiting_processing);
}
