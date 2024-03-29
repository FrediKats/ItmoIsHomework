﻿#pragma once
#include <string>
#include <CL/cl.h>

class error_validator
{
public:
	bool is_success;

	explicit error_validator(bool is_success);

	void or_throw(const std::string& message) const;
};

inline error_validator validate_error(const int error)
{
	return error_validator(error == CL_SUCCESS);
}