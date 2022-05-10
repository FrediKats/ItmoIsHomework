#include "error_validator.h"

#include "ocl_exception.h"

error_validator::error_validator(const bool is_success): is_success(is_success)
{
}

void error_validator::or_throw(const std::string& message) const
{
	if (is_success)
		return;

	throw ocl1::ocl_exception(message);
}

void error_validator::or_throw(error_message message) const
{
	or_throw(error_message_to_string(message));
}
