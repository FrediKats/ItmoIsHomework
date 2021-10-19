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

		explicit device(cl_device_id id);

		std::string to_string() const;

	private:
		std::string get_value(cl_platform_info device_info_type) const;
		bool get_bool_value(cl_platform_info device_info_type) const;
	};
}
