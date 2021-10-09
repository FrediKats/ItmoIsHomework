#pragma once
#include <CL/cl.h>

class cl_device_provider
{
public:
	cl_device_id get_device_id(int requested_index);
};
