﻿#pragma once
#include "execution_context.h"

class kernel_response
{
public:
	virtual void setup(execution_context execution_context_instance, cl_kernel kernel) = 0;
	virtual void read_result(execution_context execution_context_instance) = 0;
};
