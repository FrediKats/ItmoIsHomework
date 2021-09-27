#pragma once

#include <functional>

#include "color.h"
#include <vector>

namespace omp2
{
	class color_normalizer
	{
	public:
		std::vector<color> modify(std::vector<color> input_colors, int delta, int coefficient);

	private:
		static unsigned char change_color(std::vector<color> input_colors, size_t index, int delta, int coefficient, const std::function<unsigned char(color)>
		                                  & color_selector);
	};

	
}
