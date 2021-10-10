#include "kernel_file_source.h"

#include <fstream>
#include <utility>

kernel_file_source::kernel_file_source(std::string file_path): file_path_(std::move(file_path))
{
}

//NB: https://stackoverflow.com/a/28344440
std::string kernel_file_source::get_kernel_source_code()
{
	std::ifstream ifs(file_path_);
	try
	{
		auto result = std::string((std::istreambuf_iterator<char>(ifs)), (std::istreambuf_iterator<char>()));;
		ifs.close();
		return result;
	}
	catch (...)
	{
		ifs.close();
		throw;
	}
}
