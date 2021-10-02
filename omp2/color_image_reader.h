#pragma once
#include <string>

#include "image_descriptor.h"

namespace omp2
{
	class color_image_reader
	{
	public:
		explicit color_image_reader(std::string file_path);

		image_descriptor read() const;

	private:
		std::string file_path_;
	};
}
