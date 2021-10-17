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
		bool is_unified_memory_subsystem;

		device();
		device(cl_device_id id, std::string name, bool is_unified_memory_subsystem);
	};
}
