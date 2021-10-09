#pragma once
#include "kernel_source.h"

class kernel_file_source : public kernel_source
{
public:
	explicit kernel_file_source(std::string file_path);

	std::string get_kernel_source_code() override;
private:
	std::string file_path_;
};
