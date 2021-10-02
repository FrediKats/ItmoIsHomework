#pragma once

#include "color.h"
#include <vector>

namespace omp2
{
	class color_normalizer
	{
	public:
		static constexpr int ignore_percent = 2560;

		static std::vector<color> modify(const std::vector<color>& input_colors);
	};
}
