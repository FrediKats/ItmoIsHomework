#pragma once

#include <exception>
#include <string>

class ocl_exception final : public std::exception
{
public:
	explicit ocl_exception(const std::string& message) : exception(message.c_str())
	{
	}
};
