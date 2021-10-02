#pragma once
#include <functional>
#include <vector>

#include "color.h"

namespace omp2
{
	class color_histogram
	{
	public:
		color_histogram(std::vector<color> colors, const std::function<unsigned char(color)>& color_selector);

		unsigned char get_max_value();
		unsigned char get_min_value();

	private:
		std::vector<color> colors_;
		const std::function<unsigned char(color)>& color_selector_;
	};
}