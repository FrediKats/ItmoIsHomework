#include "device_provider.h"

#include <exception>
#include <iostream>
#include <vector>

#include "error_validator.h"

namespace ocl1
{
	std::vector<cl_platform_id> get_platforms(const bool trace_detailed_info)
	{
		cl_uint num_platforms;
		cl_int err = clGetPlatformIDs(0, nullptr, &num_platforms);
		validate_error(err).or_throw(about_create_device_group);

		//TODO: clean resources
		auto cl_selected_platform_id = static_cast<cl_platform_id*>(malloc(sizeof(cl_platform_id) * num_platforms));
		err = clGetPlatformIDs(num_platforms, cl_selected_platform_id, nullptr);
		validate_error(err).or_throw(about_create_device_group);

		// NB: https://stackoverflow.com/a/25926679
		//TODO: add flag from ctor for extended logs
		const char* attribute_names[5] = { "Name", "Vendor", "Version", "Profile", "Extensions" };
		constexpr cl_platform_info attribute_types[5] = {
			CL_PLATFORM_NAME,
			CL_PLATFORM_VENDOR,
			CL_PLATFORM_VERSION,
			CL_PLATFORM_PROFILE,
			CL_PLATFORM_EXTENSIONS
		};

		auto result = std::vector<cl_platform_id>(num_platforms);
		for (cl_uint i = 0; i < num_platforms; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				//TODO: add error validation
				size_t info_size;
				clGetPlatformInfo(cl_selected_platform_id[i], attribute_types[j], 0, nullptr, &info_size);
				//TODO: clean resources
				const auto info = static_cast<char*>(malloc(info_size));

				clGetPlatformInfo(cl_selected_platform_id[i], attribute_types[j], info_size, info, nullptr);

				if (trace_detailed_info)
					printf("  %d.%d %-11s: %s\n", i + 1, j + 1, attribute_names[j], info);

				result[i] = cl_selected_platform_id[i];
			}
		}

		return result;
	}

	//NB: https://www.khronos.org/registry/OpenCL/specs/opencl-1.2.pdf
	// CL_DEVICE_HOST_UNIFIED_MEMORY cl_bool Is CL_TRUE if the device and the host have a unified memory subsystemand is CL_FALSE otherwise
	std::vector<device> get_devices(cl_platform_id platform_id, cl_device_type device_type, bool use_unified_memory_subsystem, const bool trace_detailed_info)
	{
		cl_uint device_count;
		cl_int err = clGetDeviceIDs(platform_id, device_type, 0, nullptr, &device_count);

		// NB: in some case we request CPU-type devices from GPU devices 
		if (err == CL_DEVICE_NOT_FOUND)
			return {};

		validate_error(err).or_throw(about_getting_platform_devices);

		//TODO: clean resources
		auto device_ids = static_cast<cl_device_id*>(malloc(sizeof(cl_device_id) * device_count));
		err = clGetDeviceIDs(platform_id, device_type, device_count, device_ids, &device_count);
		validate_error(err).or_throw(about_getting_platform_devices);

		auto result = std::vector<device>(device_count);
		for (cl_uint i = 0; i < device_count; i++)
		{
			//TODO: clean resources
			// TODO: CL_DEVICE_HOST_UNIFIED_MEMORY
			//CL_DEVICE_HOST_UNIFIED_MEMORY

			size_t return_size;
			err = clGetDeviceInfo(device_ids[i], CL_DEVICE_NAME, 0, nullptr, &return_size);
			validate_error(err).or_throw(about_getting_device_info);
			const auto c_buffer = static_cast<char*>(malloc(sizeof(char) * return_size));
			err = clGetDeviceInfo(device_ids[i], CL_DEVICE_NAME, return_size, c_buffer, &return_size);
			validate_error(err).or_throw(about_getting_device_info);

			bool is_unified_memory_subsystem;
			err = clGetDeviceInfo(device_ids[i], CL_DEVICE_HOST_UNIFIED_MEMORY, 0, &is_unified_memory_subsystem, nullptr);
			validate_error(err).or_throw(about_getting_device_info);

			if (trace_detailed_info)
				std::cout << "Device " << i << ": " << c_buffer << " (" << is_unified_memory_subsystem << ")" << std::endl;

			if (use_unified_memory_subsystem != is_unified_memory_subsystem)
				continue;

			result[i] = device(device_ids[i], std::string(c_buffer), is_unified_memory_subsystem);
		}

		return result;
	}

	device device_provider::select_device(const int requested_index, const bool trace_detailed_info)
	{
		auto devices = std::vector<device>();
		const auto cl_platform_ids = get_platforms(trace_detailed_info);

		for (size_t i = 0; i < cl_platform_ids.size(); i++)
		{
			auto devices_from_platform = get_devices(cl_platform_ids[i], true, CL_DEVICE_TYPE_GPU, trace_detailed_info);
			devices.insert(devices.end(), devices_from_platform.begin(), devices_from_platform.end());
		}

		for (size_t i = 0; i < cl_platform_ids.size(); i++)
		{
			auto devices_from_platform = get_devices(cl_platform_ids[i], false, CL_DEVICE_TYPE_GPU, trace_detailed_info);
			devices.insert(devices.end(), devices_from_platform.begin(), devices_from_platform.end());
		}

		for (size_t i = 0; i < cl_platform_ids.size(); i++)
		{
			auto devices_from_platform = get_devices(cl_platform_ids[i], false, CL_DEVICE_TYPE_CPU, trace_detailed_info);
			devices.insert(devices.end(), devices_from_platform.begin(), devices_from_platform.end());
		}

		return devices[requested_index];
	}
}
