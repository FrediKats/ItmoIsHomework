#pragma once
#include <string>

#include "pnm_image_descriptor.h"

namespace omp2
{
	class color_image_writer
	{
	public:
		//TODO: add const
		explicit color_image_writer(std::string file_path);

		void write(pnm_image_descriptor image) const;

	private:
		std::string file_path_;
	};
}
