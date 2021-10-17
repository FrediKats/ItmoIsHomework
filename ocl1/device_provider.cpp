#include "device_provider.h"

#include <exception>
#include <iostream>
#include <vector>

namespace ocl1
{
	std::vector<cl_platform_id> get_platforms()
	{
		cl_uint num_platforms;
		cl_int err = clGetPlatformIDs(0, nullptr, &num_platforms);
		if (err != CL_SUCCESS)
		{
			throw std::exception("Error: Failed to create a device group!");
		}

		//TODO: clean resources
		auto cl_selected_platform_id = static_cast<cl_platform_id*>(malloc(sizeof(cl_platform_id) * num_platforms));
		err = clGetPlatformIDs(num_platforms, cl_selected_platform_id, nullptr);
		if (err != CL_SUCCESS)
			throw std::exception("Error: Failed to create a device group!");

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
				size_t info_size;
				clGetPlatformInfo(cl_selected_platform_id[i], attribute_types[j], 0, nullptr, &info_size);
				//TODO: clean resources
				const auto info = static_cast<char*>(malloc(info_size));

				clGetPlatformInfo(cl_selected_platform_id[i], attribute_types[j], info_size, info, nullptr);

				printf("  %d.%d %-11s: %s\n", i + 1, j + 1, attribute_names[j], info);
				result[i] = cl_selected_platform_id[i];
			}
		}

		return result;
	}

	std::vector<device> get_devices(cl_platform_id platform_id, cl_device_type device_type)
	{
		cl_uint device_count;
		cl_int err = clGetDeviceIDs(platform_id, device_type, 0, nullptr, &device_count);

		// NB: in some case we request CPU-type devices from GPU devices 
		if (err == CL_DEVICE_NOT_FOUND)
			return {};

		if (err != CL_SUCCESS)
			throw std::exception("Error: Failed to get devices ids for platform ");

		//TODO: clean resources
		auto device_ids = static_cast<cl_device_id*>(malloc(sizeof(cl_device_id) * device_count));
		err = clGetDeviceIDs(platform_id, device_type, device_count, device_ids, &device_count);
		if (err != CL_SUCCESS)
			throw std::exception("Error: Failed to get devices ids for platform ");

		auto result = std::vector<device>(device_count);
		for (cl_uint i = 0; i < device_count; i++)
		{
			size_t return_size;
			err = clGetDeviceInfo(device_ids[i], CL_DEVICE_NAME, 0, nullptr, &return_size);
			if (err != CL_SUCCESS)
				throw std::exception("Error: Failed to get device info");

			//TODO: clean resources
			const auto c_buffer = static_cast<char*>(malloc(sizeof(char) * return_size));
			err = clGetDeviceInfo(device_ids[i], CL_DEVICE_NAME, return_size, c_buffer, &return_size);
			if (err != CL_SUCCESS)
				throw std::exception("Error: Failed to get device info");

			result[i] = device(device_ids[i], std::string(c_buffer));
		}

		return result;
	}

	device device_provider::select_device(const int requested_index)
	{
		//TODO: detect discrete and integrated cards

		auto devices = std::vector<device>();
		const auto cl_platform_ids = get_platforms();

		for (size_t i = 0; i < cl_platform_ids.size(); i++)
		{
			auto devices_from_platform = get_devices(cl_platform_ids[i], CL_DEVICE_TYPE_GPU);
			devices.insert(devices.end(), devices_from_platform.begin(), devices_from_platform.end());
		}

		for (size_t i = 0; i < cl_platform_ids.size(); i++)
		{
			auto devices_from_platform = get_devices(cl_platform_ids[i], CL_DEVICE_TYPE_CPU);
			devices.insert(devices.end(), devices_from_platform.begin(), devices_from_platform.end());
		}

		return devices[requested_index];
	}
}
