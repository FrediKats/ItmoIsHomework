#pragma once
#include <string>
#include <CL/cl.h>

class device
{
public:
	device();
	device(cl_device_id id, const std::string& name);

	cl_device_id id;
	std::string name;
};
