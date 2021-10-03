#pragma once
#include <vector>

#include "color.h"

namespace omp2
{
	class pnm_image_descriptor
	{
	public:
		pnm_image_descriptor(int version, std::vector<color> color, int width, int height, int max_value);
		pnm_image_descriptor update_colors(std::vector<color> new_colors) const;

		const int version;
		const std::vector<color> color;
		const int width;
		const int height;
		const int max_value;
	};
}
