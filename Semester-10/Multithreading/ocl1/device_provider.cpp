#include "device_provider.h"

#include <iostream>
#include <vector>

#include "error_validator.h"
#include "platform_info.h"

namespace ocl1
{
	std::vector<platform_info> get_platforms()
	{
		cl_uint num_platforms;
		cl_int err = clGetPlatformIDs(0, nullptr, &num_platforms);
		validate_error(err).or_throw(about_create_device_group);

		cl_platform_id* cl_selected_platform_id = new cl_platform_id[num_platforms];
		err = clGetPlatformIDs(num_platforms, cl_selected_platform_id, nullptr);
		validate_error(err).or_throw(about_create_device_group);

		auto result = std::vector<platform_info>(num_platforms);
		for (cl_uint i = 0; i < num_platforms; i++)
		{
			platform_info platform(cl_selected_platform_id[i]);
			result[i] = platform;
		}

		//TODO: delete on exceptions
		delete[] cl_selected_platform_id;
		return result;
	}

	//NB: https://www.khronos.org/registry/OpenCL/specs/opencl-1.2.pdf
	// CL_DEVICE_HOST_UNIFIED_MEMORY cl_bool Is CL_TRUE if the device and the host have a unified memory subsystemand is CL_FALSE otherwise
	std::vector<device> get_devices(platform_info platform, cl_device_type device_type, bool use_unified_memory_subsystem)
	{
		cl_uint device_count;
		cl_int err = clGetDeviceIDs(platform.id, device_type, 0, nullptr, &device_count);

		// NB: in some case we request CPU-type devices from GPU devices 
		if (err == CL_DEVICE_NOT_FOUND)
			return {};

		validate_error(err).or_throw(about_getting_platform_devices);

		cl_device_id* device_ids = new cl_device_id[device_count];
		err = clGetDeviceIDs(platform.id, device_type, device_count, device_ids, &device_count);
		validate_error(err).or_throw(about_getting_platform_devices);

		auto result = std::vector<device>(device_count);
		for (cl_uint i = 0; i < device_count; i++)
		{
			const device current_device(device_ids[i]);
			//TODO: filter by use_unified_memory_subsystem
			result[i] = current_device;
		}

		//TODO: delete on exceptions
		delete[] device_ids;
		return result;
	}

	device_provider::device_provider(const bool trace_detailed_info): trace_detailed_info_(trace_detailed_info)
	{
	}

	device device_provider::select_device(const int requested_index) const
	{
		auto devices = std::vector<device>();
		const std::vector<platform_info> cl_platform_ids = get_platforms();

		// NB: https://stackoverflow.com/a/25926679
		//TODO: add flag from ctor for extended logs
		for (const auto& cl_platform_id : cl_platform_ids)
		{
			if (trace_detailed_info_)
				std::cout << cl_platform_id.to_string();
		}

		for (size_t i = 0; i < cl_platform_ids.size(); i++)
		{
			auto devices_from_platform = get_devices(cl_platform_ids[i], CL_DEVICE_TYPE_GPU, true);
			devices.insert(devices.end(), devices_from_platform.begin(), devices_from_platform.end());
		}

		for (size_t i = 0; i < cl_platform_ids.size(); i++)
		{
			auto devices_from_platform = get_devices(cl_platform_ids[i], CL_DEVICE_TYPE_GPU, false);
			devices.insert(devices.end(), devices_from_platform.begin(), devices_from_platform.end());
		}

		for (size_t i = 0; i < cl_platform_ids.size(); i++)
		{
			auto devices_from_platform = get_devices(cl_platform_ids[i], CL_DEVICE_TYPE_CPU, false);
			devices.insert(devices.end(), devices_from_platform.begin(), devices_from_platform.end());
		}

		for (const auto& device : devices)
		{
			if (trace_detailed_info_)
				std::cout << device.to_string();
		}

		return devices[requested_index];
	}
}
