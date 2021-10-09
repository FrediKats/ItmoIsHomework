#define CL_USE_DEPRECATED_OPENCL_1_2_APIS

#include <fstream>
#include <iostream>
#include <CL/opencl.h>

//NB: https://stackoverflow.com/a/28344440
std::string get_file_string() {
    std::ifstream ifs("kernel.txt");
    return std::string((std::istreambuf_iterator<char>(ifs)),
        (std::istreambuf_iterator<char>()));
}

int main()
{
    int err;
    cl_context context;
    cl_uint numPlatforms;
    cl_platform_id* clSelectedPlatformID = NULL;
    cl_device_id device_id;
    cl_command_queue commands;
    //get Platform
    clGetPlatformIDs(0, NULL, &numPlatforms);
    clSelectedPlatformID = (cl_platform_id*)malloc(sizeof(cl_platform_id) * numPlatforms);
    err = clGetPlatformIDs(numPlatforms, clSelectedPlatformID, NULL);

    //get Device
    err = clGetDeviceIDs(clSelectedPlatformID[0], CL_DEVICE_TYPE_GPU, 1, &device_id, NULL);
    if (err != CL_SUCCESS)
    {
        printf("Error: Failed to create a device group!\n");
        return EXIT_FAILURE;
    }

    //create context
    context = clCreateContext(0, 1, &device_id, NULL, NULL, &err);
    if (!context)
    {
        printf("Error: Failed to create a compute context!\n");
        return EXIT_FAILURE;
    }
    //
    commands = clCreateCommandQueue(context, device_id, 0, &err);
    if (!commands)
    {
        printf("Error: Failed to create a command commands!\n");
        return EXIT_FAILURE;
    }

    std::string data = get_file_string();
    const char* KernelSource = data.c_str();
    //TODO: prebuild
    cl_program program = clCreateProgramWithSource(context, 1, &KernelSource, NULL, &err);
    if (!program)
    {
        printf("Error: Failed to create compute program!\n");
        return EXIT_FAILURE;
    }

    err = clBuildProgram(program, 0, NULL, NULL, NULL, NULL);
    int a = 1;
    int b = 2;
    cl_kernel kernel = clCreateKernel(program, "allToOne", &err);
    clSetKernelArg(kernel, 0, 4, &a);
    clSetKernelArg(kernel, 1, 4, &b);
    size_t global_item_size = 4;
    clEnqueueNDRangeKernel(commands, kernel, 1, NULL, &global_item_size, NULL, 0, NULL, NULL);


    return 0;
}
