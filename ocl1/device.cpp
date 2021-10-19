#include "device.h"

#include "error_validator.h"
#include "ocl_exception.h"

namespace ocl1
{
	device::device()
	{
	}

	device::device(cl_device_id id)
	{
		name = get_value(CL_DEVICE_NAME);
		is_unified_memory_subsystem = get_bool_value(CL_DEVICE_HOST_UNIFIED_MEMORY);
	}

	std::string device::to_string() const
	{
		return "Device" + name + " (" + "is_unified_memory_subsystem: " + std::to_string(is_unified_memory_subsystem) + ")";
	}

	std::string device::get_value(cl_platform_info device_info_type) const
	{
		char* info = nullptr;
		try
		{
			size_t return_size;
			int err = clGetDeviceInfo(id, device_info_type, 0, nullptr, &return_size);
			validate_error(err).or_throw(about_getting_device_info);
			info = new char[return_size];
			err = clGetDeviceInfo(id, device_info_type, return_size, info, &return_size);
			validate_error(err).or_throw(about_getting_device_info);

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

	bool device::get_bool_value(cl_platform_info device_info_type) const
	{
		//TODO: fix
		return false;

		size_t return_size;
		int err = clGetDeviceInfo(id, device_info_type, 0, nullptr, &return_size);
		validate_error(err).or_throw(about_getting_device_info);

		if (return_size != 4)
			throw ocl_exception("Unexpected bool size" + std::to_string(return_size));

		bool result = false;
		err = clGetDeviceInfo(id, device_info_type, return_size, &result, &return_size);
		validate_error(err).or_throw(about_getting_device_info);

		return result;
	}
}
