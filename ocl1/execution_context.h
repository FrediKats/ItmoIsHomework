#pragma once
#include "device.h"

class execution_context
{
public:
	explicit execution_context(device device);

	cl_context context;
	cl_command_queue command_queue;
};
