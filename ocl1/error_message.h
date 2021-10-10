#pragma once

#include <string>

enum error_message
{
	about_kernel_response_setup,
	about_kernel_response_read,
	about_kernel_argument_create_buffer,
	about_kernel_argument_write,
	about_set_kernel_argument,
	about_create_program,
	about_create_kernel,
	about_kernel_enqueue,
	about_waiting_processing,
};

inline std::string error_message_to_string(const error_message& message)
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
	case about_create_program:
		return "Error: Failed to create compute program";
	case about_create_kernel:
		return "Error: Failed to create kernel";
	case about_kernel_enqueue:
		return "Error: Failed to enqueue kernel";
	case about_waiting_processing:
		return "Error: Failed while wait cl execute result";

	default:
		throw std::exception(("Unexpected error message type: " + std::to_string(message)).c_str());
	}
}
