#include "platform_info.h"

#include "error_validator.h"

ocl1::platform_info::platform_info(cl_platform_id id): id(id)
{
	// NB: https://stackoverflow.com/a/25926679
	name = get_value(CL_PLATFORM_NAME);
	vendor = get_value(CL_PLATFORM_VENDOR);
	version = get_value(CL_PLATFORM_VERSION);
	profile = get_value(CL_PLATFORM_PROFILE);
	extensions = get_value(CL_PLATFORM_EXTENSIONS);
}

std::string ocl1::platform_info::to_string() const
{
	return name + "(" + vendor + ")" "{ Version: " + version + ", Profile: " + profile + ", Extensions: " + extensions;
}

std::string ocl1::platform_info::get_value(cl_platform_info platform_info_type) const
{
	char* info = nullptr;
	try
	{
		size_t info_size;
		int err = clGetPlatformInfo(id, platform_info_type, 0, nullptr, &info_size);
		validate_error(err).or_throw(about_getting_platform_devices);

		info = new char[info_size];
		err = clGetPlatformInfo(id, platform_info_type, info_size, info, nullptr);
		validate_error(err).or_throw(about_getting_platform_devices);

		auto result = std::string(info);
		delete info;
		return result;
	}
	catch (...)
	{
		delete info;
		throw;
	}
}
