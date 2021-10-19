#include "device_provider.h"

#include <exception>
#include <iostream>
#include <vector>

#include "error_validator.h"
#include "platform_info.h"

namespace ocl1
{
	std::vector<platform_info> get_platforms(const bool trace_detailed_info)
	{
		cl_uint num_platforms;
		cl_int err = clGetPlatformIDs(0, nullptr, &num_platforms);
		validate_error(err).or_throw(about_create_device_group);

		//TODO: clean resources
		const auto cl_selected_platform_id = new cl_platform_id[num_platforms];
		err = clGetPlatformIDs(num_platforms, cl_selected_platform_id, nullptr);
		validate_error(err).or_throw(about_create_device_group);

		// NB: https://stackoverflow.com/a/25926679
		//TODO: add flag from ctor for extended logs
		auto result = std::vector<platform_info>(num_platforms);
		for (cl_uint i = 0; i < num_platforms; i++)
		{
			platform_info platform(cl_selected_platform_id[i]);
			if (trace_detailed_info)
				std::cout << platform.to_string();

			result[i] = platform;
		}

		return result;
	}

	//NB: https://www.khronos.org/registry/OpenCL/specs/opencl-1.2.pdf
	// CL_DEVICE_HOST_UNIFIED_MEMORY cl_bool Is CL_TRUE if the device and the host have a unified memory subsystemand is CL_FALSE otherwise
	std::vector<device> get_devices(platform_info platform, cl_device_type device_type, bool use_unified_memory_subsystem, const bool trace_detailed_info)
	{
		cl_uint device_count;
		cl_int err = clGetDeviceIDs(platform.id, device_type, 0, nullptr, &device_count);

		// NB: in some case we request CPU-type devices from GPU devices 
		if (err == CL_DEVICE_NOT_FOUND)
			return {};

		validate_error(err).or_throw(about_getting_platform_devices);

		//TODO: clean resources
		cl_device_id* device_ids = new cl_device_id[device_count];
		err = clGetDeviceIDs(platform.id, device_type, device_count, device_ids, &device_count);
		validate_error(err).or_throw(about_getting_platform_devices);

		auto result = std::vector<device>(device_count);
		for (cl_uint i = 0; i < device_count; i++)
		{
			device current_device(device_ids[i]);
			if (trace_detailed_info)
				std::cout << "Device " << current_device.to_string() << std::endl;

			result[i] = current_device;
		}

		return result;
	}

	device device_provider::select_device(const int requested_index, const bool trace_detailed_info)
	{
		auto devices = std::vector<device>();
		const std::vector<platform_info> cl_platform_ids = get_platforms(trace_detailed_info);

		for (size_t i = 0; i < cl_platform_ids.size(); i++)
		{
			auto devices_from_platform = get_devices(cl_platform_ids[i], CL_DEVICE_TYPE_GPU, true, trace_detailed_info);
			devices.insert(devices.end(), devices_from_platform.begin(), devices_from_platform.end());
		}

		for (size_t i = 0; i < cl_platform_ids.size(); i++)
		{
			auto devices_from_platform = get_devices(cl_platform_ids[i], CL_DEVICE_TYPE_GPU, false, trace_detailed_info);
			devices.insert(devices.end(), devices_from_platform.begin(), devices_from_platform.end());
		}

		for (size_t i = 0; i < cl_platform_ids.size(); i++)
		{
			auto devices_from_platform = get_devices(cl_platform_ids[i], CL_DEVICE_TYPE_CPU, false, trace_detailed_info);
			devices.insert(devices.end(), devices_from_platform.begin(), devices_from_platform.end());
		}

		return devices[requested_index];
	}
}
