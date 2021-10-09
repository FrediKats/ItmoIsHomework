#define CL_USE_DEPRECATED_OPENCL_1_2_APIS

#include <fstream>
#include <iostream>
#include <CL/opencl.h>
#include <vector>

//NB: https://stackoverflow.com/a/28344440
std::string get_file_string() {
    std::ifstream ifs("kernel.txt");
    return std::string((std::istreambuf_iterator<char>(ifs)),
        (std::istreambuf_iterator<char>()));
}

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
    for (int j = 0; j < 5; j++)
    {
        for (int i = 0; i < num_platforms; i++)
        {
            // get platform attribute value size
            clGetPlatformInfo(cl_selected_platform_id[i], attributeTypes[j], 0, NULL, &infoSize);
            auto info = (char*)malloc(infoSize);

            // get platform attribute value
            clGetPlatformInfo(cl_selected_platform_id[i], attributeTypes[j], infoSize, info, NULL);

            //printf("  %d.%d %-11s: %s\n", i + 1, j + 1, attributeNames[j], info);
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
    err = clGetDeviceIDs(platform_id, device_type, 0, device_ids, &device_count);
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

cl_device_id select_device(cl_platform_id platform_id)
{
    cl_uint device_count;
    cl_device_id device_id;
    //TODO: select discrete video card
    // use clGetDeviceIDS | clGetDeviceInfo
    cl_int err = clGetDeviceIDs(platform_id, CL_DEVICE_TYPE_GPU, 0, &device_id, &device_count);
    if (err != CL_SUCCESS)
    {
        throw std::exception("Error: Failed to create a device group!");
    }

    return device_id;
}

int main()
{

    int err;
    cl_context context;
    cl_command_queue command_queue;


    const auto cl_platform_ids = get_platforms();
    //TODO: fix [0]
    for (int i = 0; i < cl_platform_ids.size(); i++)
    {
        select_device(cl_platform_ids[i], CL_DEVICE_TYPE_GPU);
    }
	cl_device_id device_id = select_device(cl_platform_ids[0], CL_DEVICE_TYPE_GPU);


    //create context
    //TODO: need to dispose this context
    context = clCreateContext(nullptr, 1, &device_id, nullptr, nullptr, &err);
    if (!context)
    {
        printf("Error: Failed to create a compute context!\n");
        return EXIT_FAILURE;
    }

    // TODO: Allow profiling
    command_queue = clCreateCommandQueue(context, device_id, 0, &err);
    if (!command_queue)
    {
        printf("Error: Failed to create a command commands!\n");
        return EXIT_FAILURE;
    }

    std::string data = get_file_string();
    const char* kernel_source = data.c_str();
    //TODO: prebuild
    cl_program program = clCreateProgramWithSource(context, 1, &kernel_source, nullptr, &err);
    if (!program)
    {
        printf("Error: Failed to create compute program!\n");
        return EXIT_FAILURE;
    }

    //TODO: handle error
    //TODO: clGetProgramBuildLog
    err = clBuildProgram(program, 0, nullptr, nullptr, nullptr, nullptr);

    int a = 1;
    int b = 2;
    int c = 0;

    // -37 - invalid host ptr
    cl_mem input_a = clCreateBuffer(context, CL_MEM_READ_ONLY, sizeof(int), nullptr, &err);
    cl_mem input_b = clCreateBuffer(context, CL_MEM_READ_ONLY, sizeof(int), nullptr, &err);
    cl_mem output = clCreateBuffer(context, CL_MEM_WRITE_ONLY, sizeof(int), nullptr, &err);

    err = clEnqueueWriteBuffer(command_queue, input_a, CL_TRUE, 0, sizeof(int), &a, 0, nullptr, nullptr);
    err = clEnqueueWriteBuffer(command_queue, input_b, CL_TRUE, 0, sizeof(int), &b, 0, nullptr, nullptr);

    cl_kernel kernel = clCreateKernel(program, "sum", &err);
    clSetKernelArg(kernel, 0, sizeof(cl_mem), &input_a);
    clSetKernelArg(kernel, 1, sizeof(cl_mem), &input_b);
    clSetKernelArg(kernel, 2, sizeof(cl_mem), &output);

    size_t global_item_size = 4;
    size_t local_item_size = 4;
    err = clEnqueueNDRangeKernel(command_queue, kernel, 1, nullptr, &global_item_size, &local_item_size, 0, nullptr, nullptr);
    clFinish(command_queue);
    err = clEnqueueReadBuffer(command_queue, output, CL_TRUE, 0, sizeof(int), &c, 0, nullptr, nullptr);

    //auto buffer = clCreateBuffer(context, 0, sizeof(cl_int), nullptr, nullptr);

    std::cout << c;

    //TODO: release memory object
    //auto buffer = clCreateBuffer(context, 0, sizeof(cl_int), nullptr, nullptr);
    //TODO: add sync | async
    //cl_int write_byte_count = clEnqueueWriteBuffer(command_queue, buffer, false, 0, sizeof(cl_int), nullptr, 0, nullptr, nullptr);
    //cl_int read_byte_count = clEnqueueReadBuffer(command_queue, buffer, false, 0, sizeof(cl_int), nullptr, 0, nullptr, nullptr);

	return 0;
}
