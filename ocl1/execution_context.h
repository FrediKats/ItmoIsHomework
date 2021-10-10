#pragma once
#include "device.h"

class execution_context
{
public:
	cl_context context;
	cl_command_queue command_queue;
	device selected_device;

	explicit execution_context(device device);
};
