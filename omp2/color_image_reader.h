#pragma once
#include <string>

#include "pnm_image_descriptor.h"

namespace omp2
{
	class color_image_reader
	{
	public:
		explicit color_image_reader(std::string file_path);

		pnm_image_descriptor read() const;

	private:
		std::string file_path_;
	};
}
