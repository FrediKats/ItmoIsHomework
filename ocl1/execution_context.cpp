#define CL_USE_DEPRECATED_OPENCL_1_2_APIS

#include "execution_context.h"

#include "error_message.h"
#include "error_validator.h"
#include "ocl_exception.h"

namespace ocl1
{
	execution_context::execution_context(device device, const kernel_dimension_config dimension_config): dimension_config(dimension_config)
	{
		int err;
		//TODO: need to dispose this context
		context = clCreateContext(nullptr, 1, &device.id, nullptr, nullptr, &err);
		validate_error(err).or_throw(about_create_context);
		if (!context)
			throw ocl_exception(error_message_to_string(about_create_context));

		command_queue = clCreateCommandQueue(context, device.id, CL_QUEUE_PROFILING_ENABLE, &err);
		validate_error(err).or_throw(about_create_command_queue);
		if (!command_queue)
			throw ocl_exception(error_message_to_string(about_create_command_queue));

		selected_device = device;
	}
	
}
