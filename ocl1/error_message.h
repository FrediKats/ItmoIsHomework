#pragma once

#include <string>

enum error_message
{
	about_kernel_response_setup,
};

inline std::string error_message_to_string(const error_message message)
{
	switch (message)
	{
	case about_kernel_response_setup:
		return "Error while setup kernel response";

	default:
		throw std::exception(("Unexpected error message type: " + std::to_string(message)).c_str());
	}
}
