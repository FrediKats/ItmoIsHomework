#pragma once
#include <vector>

#include "color.h"

namespace omp2
{
	class image_descriptor
	{
	public:
		image_descriptor(std::vector<color> color, int width, int height);

		const std::vector<color> color;
		const int width;
		const int height;
	};
}
