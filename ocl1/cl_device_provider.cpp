#include "cl_device_provider.h"

#include <exception>
#include <iostream>
#include <vector>

std::vector<cl_platform_id> get_platforms()
{
    cl_uint num_platforms;
    cl_platform_id* cl_selected_platform_id = nullptr;
    // The only one way for detecting platform count
    clGetPlatformIDs(0, nullptr, &num_platforms);

    cl_selected_platform_id = static_cast<cl_platform_id*>(malloc(sizeof(cl_platform_id) * num_platforms));
    const int err = clGetPlatformIDs(num_platforms, cl_selected_platform_id, nullptr);
    if (err != CL_SUCCESS)
    {
        throw std::exception("Error: Failed to create a device group!");
    }

    auto result = std::vector<cl_platform_id>(num_platforms);
    // NB: https://stackoverflow.com/a/25926679
    const char* attributeNames[5] = { "Name", "Vendor", "Version", "Profile", "Extensions" };
    const cl_platform_info attributeTypes[5] = {
        CL_PLATFORM_NAME,
                                                CL_PLATFORM_VENDOR,
                                                CL_PLATFORM_VERSION,
                                                CL_PLATFORM_PROFILE,
                                                CL_PLATFORM_EXTENSIONS };

    size_t infoSize;
    for (int i = 0; i < num_platforms; i++)
    {
        for (int j = 0; j < 5; j++)
        {
            // get platform attribute value size
            clGetPlatformInfo(cl_selected_platform_id[i], attributeTypes[j], 0, NULL, &infoSize);
            auto info = (char*)malloc(infoSize);

            // get platform attribute value
            clGetPlatformInfo(cl_selected_platform_id[i], attributeTypes[j], infoSize, info, NULL);

            printf("  %d.%d %-11s: %s\n", i + 1, j + 1, attributeNames[j], info);
            result[i] = cl_selected_platform_id[i];
        }
    }


    return result;
}

cl_device_id select_device(cl_platform_id platform_id, cl_device_type device_type)
{
    cl_uint device_count;
    //TODO: select discrete video card
    // use clGetDeviceIDS | clGetDeviceInfo
    cl_int err = clGetDeviceIDs(platform_id, device_type, 0, nullptr, &device_count);

    cl_device_id* device_ids = static_cast<cl_device_id*>(malloc(sizeof(cl_device_id) * device_count));
    err = clGetDeviceIDs(platform_id, device_type, device_count, device_ids, &device_count);
    for (int i = 0; i < device_count; i++)
    {
        size_t return_size;
        clGetDeviceInfo(device_ids[i], CL_DEVICE_NAME, 0, nullptr, &return_size);

        char* cBuffer = static_cast<char*>(malloc(sizeof(char) * return_size));
        clGetDeviceInfo(device_ids[i], CL_DEVICE_NAME, return_size, cBuffer, &return_size);
        std::string name = std::string(cBuffer);

        std::cout << name;
    }

    if (err != CL_SUCCESS)
    {
        throw std::exception("Error: Failed to create a device group!");
    }

    return device_ids[0];
}

cl_device_id cl_device_provider::get_device_id(int requested_index)
{
    //TODO: detect discrete and integrated cards


    const auto cl_platform_ids = get_platforms();
    //TODO: fix [0]
    for (int i = 0; i < cl_platform_ids.size(); i++)
    {
        select_device(cl_platform_ids[i], CL_DEVICE_TYPE_GPU);
    }
    cl_device_id device_id = select_device(cl_platform_ids[0], CL_DEVICE_TYPE_GPU);
    return device_id;
}
