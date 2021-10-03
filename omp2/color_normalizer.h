#pragma once

#include "color.h"
#include <vector>

namespace omp2
{
	class color_normalizer
	{
	public:
		virtual std::vector<color> modify(const std::vector<color>& input_colors);
	};
}
