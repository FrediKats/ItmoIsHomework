#pragma once
#include "execution_context.h"
#include "kernel_source.h"

class kernel_file_source final : public kernel_source
{
public:
	explicit kernel_file_source(std::string file_path);

	cl_kernel get_kernel_source_code(const execution_context execution_context_instance, std::string kernel_name, program_builder builder) override;
private:
	std::string file_path_;
};
