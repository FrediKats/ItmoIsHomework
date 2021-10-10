#include "error_validator.h"

error_validator::error_validator(const bool is_success): is_success(is_success)
{
}

void error_validator::or_throw(const std::string& message) const
{
	if (is_success)
		return;

	throw std::exception(message.c_str());
}

void error_validator::or_throw(const error_message message) const
{
	or_throw(error_message_to_string(message));
}
