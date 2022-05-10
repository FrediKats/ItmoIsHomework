#pragma once

#include <string>

#include "device.h"
#include "execution_context.h"

namespace ocl1
{
	class program_builder
	{
	public:
		program_builder(cl_context context, const device& device);
		explicit program_builder(execution_context context);

		cl_program build(const std::string& kernel_source_code);

	private:
		cl_context context_;
		device device_;
	};
}
