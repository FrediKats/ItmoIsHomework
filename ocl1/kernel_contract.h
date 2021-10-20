#pragma once

#include <CL/cl.h>

#include "execution_context.h"
#include "error_validator.h"
#include "error_message.h"
#include "execution_benchmark_result.h"

namespace ocl1
{
	template <typename TArguments, typename TResponse>
	class kernel_contract
	{
	public:
		kernel_contract(const execution_context& execution_context_instance, cl_kernel kernel);

		execution_benchmark_result execute(TArguments& argument, TResponse& response);

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
	execution_benchmark_result kernel_contract<TArguments, TResponse>::execute(TArguments& argument, TResponse& response)
	{
		int err;

		const auto total_start_time = std::chrono::high_resolution_clock::now();
		argument.write_arguments(execution_context_instance_, kernel_);
		response.setup(execution_context_instance_, kernel_);

		err = clFinish(execution_context_instance_.command_queue);
		validate_error(err).or_throw(error_message::about_kernel_enqueue);
		const auto kernel_start_time = std::chrono::high_resolution_clock::now();

		err = clEnqueueNDRangeKernel(
			execution_context_instance_.command_queue,
			kernel_,
			execution_context_instance_.dimension_config.size,
			nullptr,
			execution_context_instance_.dimension_config.global_size,
			execution_context_instance_.dimension_config.local_size,
			0,
			nullptr,
			nullptr);
		validate_error(err).or_throw(error_message::about_kernel_enqueue);
		err = clFinish(execution_context_instance_.command_queue);
		validate_error(err).or_throw(error_message::about_kernel_enqueue);
		const auto kernel_finish_time = std::chrono::high_resolution_clock::now();

		response.read_result(execution_context_instance_);
		err = clFinish(execution_context_instance_.command_queue);
		validate_error(err).or_throw(error_message::about_waiting_processing);
		const auto total_finish_time = std::chrono::high_resolution_clock::now();

		return execution_benchmark_result(total_start_time, kernel_start_time, kernel_finish_time, total_finish_time);
	}
}
