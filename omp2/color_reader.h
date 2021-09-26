#pragma once
#include <string>
#include <vector>

#include "color.h"

namespace omp2
{
	class color_reader
	{
	public:
		explicit color_reader(const std::string& file_path);

		std::vector<color> read() const;

	private:
		const std::string& file_path_;
	};
}
