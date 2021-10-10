#pragma once
#include <string>
#include <CL/cl.h>

class device
{
public:
	cl_device_id id;
	std::string name;

	device();
	device(cl_device_id id, std::string name);
};
