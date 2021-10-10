#pragma once

#include <string>

enum error_message
{
	about_kernel_response_setup,
	about_kernel_response_read,
	about_kernel_argument_create_buffer,
	about_kernel_argument_write,
	about_set_kernel_argument,
};

inline std::string error_message_to_string(error_message message)
{
	switch (message)
	{
	case about_kernel_response_setup:
		return "Error while setup kernel response";
	case about_kernel_response_read:
		return "Error while read kernel response";
	case about_kernel_argument_create_buffer:
		return "Error while creating write buffer";
	case about_kernel_argument_write:
		return "Error while writing kernel arguments";
	case about_set_kernel_argument:
		return "Error while setting arguments in kernel";

	default:
		throw std::exception(("Unexpected error message type: " + std::to_string(message)).c_str());
	}
}
