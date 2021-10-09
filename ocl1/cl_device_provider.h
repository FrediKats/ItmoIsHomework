#pragma once
#include <CL/cl.h>

#include "device.h"

class cl_device_provider
{
public:
	device select_device(int requested_index);
};
