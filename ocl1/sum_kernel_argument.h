﻿#pragma once
#include "execution_context.h"

class sum_kernel_argument
{
public:
	int a;
	int b;

	sum_kernel_argument(int a, int b);

	void write_arguments(execution_context execution_context_instance, cl_kernel kernel);
};
