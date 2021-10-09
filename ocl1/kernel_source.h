#pragma once
#include <string>

class kernel_source
{
public:
	virtual  std::string get_kernel_source_code() = 0;
};
