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

		unsigned char get_max_value() const;
		unsigned char get_min_value() const;

	private:
		unsigned char min_;
		unsigned char max_;
		std::vector<color> colors_;
		const std::function<unsigned char(color)>& color_selector_;
	};
}