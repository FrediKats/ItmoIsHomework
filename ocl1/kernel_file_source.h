#pragma once

#include "kernel_source.h"

namespace ocl1
{
	class kernel_file_source final : public kernel_source
	{
	public:
		explicit kernel_file_source(std::string file_path);

		cl_kernel get_kernel_source_code(std::string kernel_name, ocl1::program_builder builder) override;
	private:
		std::string file_path_;
	};
}
