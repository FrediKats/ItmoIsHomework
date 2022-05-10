#pragma once
#include <vector>

#include "color.h"

namespace omp2
{
	class pnm_image_descriptor
	{
	public:
		pnm_image_descriptor(int version, std::vector<color> color, int width, int height);

		const int version;
		const std::vector<color> color;
		const int width;
		const int height;
	};
}
