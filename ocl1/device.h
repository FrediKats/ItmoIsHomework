#pragma once

#include <CL/cl.h>
#include <string>

namespace ocl1
{
	class device
	{
	public:
		cl_device_id id;
		std::string name;

		device();
		device(cl_device_id id, std::string name);
	};
}
