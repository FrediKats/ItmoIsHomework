#pragma once
#include "device.h"
#include "kernel_dimension_config.h"

class execution_context
{
public:
	cl_context context;
	cl_command_queue command_queue;
	device selected_device;
	kernel_dimension_config dimension_config;

	explicit execution_context(device device, kernel_dimension_config dimension_config);
};
