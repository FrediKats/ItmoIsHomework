#pragma once
#include <string>

#include "image_descriptor.h"

namespace omp2
{
	class color_reader
	{
	public:
		explicit color_reader(std::string file_path);

		image_descriptor read() const;
		void write(const std::string& file_path, image_descriptor image) const;

	private:
		std::string file_path_;
	};
}
