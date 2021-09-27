#pragma once
#include <string>

#include "image_descriptor.h"

namespace omp2
{
	class color_reader
	{
	public:
		explicit color_reader(const std::string& file_path);

		image_descriptor read() const;

	private:
		const std::string& file_path_;
	};
}
