#include "cl_device_provider.h"

#include <exception>
#include <iostream>
#include <vector>

std::vector<cl_platform_id> get_platforms()
{
	cl_uint num_platforms;
    cl_int err = clGetPlatformIDs(0, nullptr, &num_platforms);
    if (err != CL_SUCCESS)
        throw std::exception("Error: Failed to create a device group!");

	cl_platform_id* cl_selected_platform_id = static_cast<cl_platform_id*>(malloc(sizeof(cl_platform_id) * num_platforms));
	err = clGetPlatformIDs(num_platforms, cl_selected_platform_id, nullptr);
	if (err != CL_SUCCESS)
		throw std::exception("Error: Failed to create a device group!");

	// NB: https://stackoverflow.com/a/25926679
	const char* attribute_names[5] = {"Name", "Vendor", "Version", "Profile", "Extensions"};
	constexpr cl_platform_info attribute_types[5] = {
		CL_PLATFORM_NAME,
		CL_PLATFORM_VENDOR,
		CL_PLATFORM_VERSION,
		CL_PLATFORM_PROFILE,
		CL_PLATFORM_EXTENSIONS
	};

	//TODO: remove output
	size_t infoSize;

    auto result = std::vector<cl_platform_id>(num_platforms);
    for (int i = 0; i < num_platforms; i++)
	{
		for (int j = 0; j < 5; j++)
		{
			// get platform attribute value size
			clGetPlatformInfo(cl_selected_platform_id[i], attribute_types[j], 0, nullptr, &infoSize);
			const auto info = static_cast<char*>(malloc(infoSize));

			// get platform attribute value
			clGetPlatformInfo(cl_selected_platform_id[i], attribute_types[j], infoSize, info, nullptr);

			printf("  %d.%d %-11s: %s\n", i + 1, j + 1, attribute_names[j], info);
			result[i] = cl_selected_platform_id[i];
		}
	}

	return result;
}

std::vector<cl_device_id> get_devices(cl_platform_id platform_id, cl_device_type device_type)
{
    cl_uint device_count;
    cl_int err = clGetDeviceIDs(platform_id, device_type, 0, nullptr, &device_count);
    if (err == CL_DEVICE_NOT_FOUND)
        return std::vector<cl_device_id>();

    if (err != CL_SUCCESS)
        throw std::exception("Error: Failed to get devices ids for platform ");
    auto device_ids = static_cast<cl_device_id*>(malloc(sizeof(cl_device_id) * device_count));
    err = clGetDeviceIDs(platform_id, device_type, device_count, device_ids, &device_count);
    if (err != CL_SUCCESS)
        throw std::exception("Error: Failed to get devices ids for platform ");

    std::vector<cl_device_id> result = std::vector<cl_device_id>(device_count);
	for (cl_uint i = 0; i < device_count; i++)
    {
        size_t return_size;
        err = clGetDeviceInfo(device_ids[i], CL_DEVICE_NAME, 0, nullptr, &return_size);
        if (err != CL_SUCCESS)
            throw std::exception("Error: Failed to get device info");

        char* cBuffer = static_cast<char*>(malloc(sizeof(char) * return_size));
        err = clGetDeviceInfo(device_ids[i], CL_DEVICE_NAME, return_size, cBuffer, &return_size);
        if (err != CL_SUCCESS)
            throw std::exception("Error: Failed to get device info");

        std::string name = std::string(cBuffer);

        std::cout << name;

        result[i] = device_ids[i];
    }

    return result;
}

cl_device_id cl_device_provider::get_device_id(int requested_index)
{
    //TODO: detect discrete and integrated cards

    std::vector<cl_device_id> devices = std::vector<cl_device_id>();
    const auto cl_platform_ids = get_platforms();

    for (int i = 0; i < cl_platform_ids.size(); i++)
    {
        auto devices_from_platform = get_devices(cl_platform_ids[i], CL_DEVICE_TYPE_GPU);
        devices.insert(devices.end(), devices_from_platform.begin(), devices_from_platform.end());
    }

    for (int i = 0; i < cl_platform_ids.size(); i++)
    {
        auto devices_from_platform = get_devices(cl_platform_ids[i], CL_DEVICE_TYPE_CPU);
        devices.insert(devices.end(), devices_from_platform.begin(), devices_from_platform.end());
    }

    return devices[requested_index];
}
