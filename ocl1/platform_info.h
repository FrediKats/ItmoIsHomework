#pragma once

#include <string>
#include <CL/cl.h>

namespace ocl1
{
	class platform_info
	{
	public:
		cl_platform_id id;

		std::string name;
		std::string vendor;
		std::string version;
		std::string profile;
		std::string extensions;

		platform_info() {  }

		explicit platform_info(cl_platform_id id);
		std::string to_string() const;

	private:
		std::string get_value(cl_platform_info platform_info_type) const;
	};
}
