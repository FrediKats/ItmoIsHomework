#define CL_USE_DEPRECATED_OPENCL_1_2_APIS

#include "execution_context.h"

execution_context::execution_context(device device)
{
	int err;
	//create context
	//TODO: need to dispose this context
	context = clCreateContext(nullptr, 1, &device.id, nullptr, nullptr, &err);
	if (!context)
		throw std::exception("Error: Failed to create a compute context!");

	// TODO: Allow profiling
	command_queue = clCreateCommandQueue(context, device.id, 0, &err);
	if (!command_queue)
		throw std::exception("Error: Failed to create a command commands!");
}
