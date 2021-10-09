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
	for (int i = 0; i < num_platforms; i++)
	{
		result[i] = cl_selected_platform_id[i];
	}

	return result;
}

int main()
{
	const auto cl_platform_ids = get_platforms();

    int err;
    cl_context context;
    cl_device_id device_id;
    cl_command_queue command_queue;

    //get Device
    err = clGetDeviceIDs(cl_platform_ids[0], CL_DEVICE_TYPE_GPU, 1, &device_id, nullptr);
    if (err != CL_SUCCESS)
    {
        printf("Error: Failed to create a device group!\n");
        return EXIT_FAILURE;
    }

    //create context
    context = clCreateContext(nullptr, 1, &device_id, nullptr, nullptr, &err);
    if (!context)
    {
        printf("Error: Failed to create a compute context!\n");
        return EXIT_FAILURE;
    }
    //
    command_queue = clCreateCommandQueue(context, device_id, 0, &err);
    if (!command_queue)
    {
        printf("Error: Failed to create a command commands!\n");
        return EXIT_FAILURE;
    }

    std::string data = get_file_string();
    const char* KernelSource = data.c_str();
    //TODO: prebuild
    cl_program program = clCreateProgramWithSource(context, 1, &KernelSource, nullptr, &err);
    if (!program)
    {
        printf("Error: Failed to create compute program!\n");
        return EXIT_FAILURE;
    }

    err = clBuildProgram(program, 0, nullptr, nullptr, nullptr, nullptr);
    int a = 1;
    int b = 2;
    cl_kernel kernel = clCreateKernel(program, "allToOne", &err);
    clSetKernelArg(kernel, 0, 4, &a);
    clSetKernelArg(kernel, 1, 4, &b);
    size_t global_item_size = 4;
    clEnqueueNDRangeKernel(command_queue, kernel, 1, nullptr, &global_item_size, nullptr, 0, nullptr, nullptr);


    return 0;
}
