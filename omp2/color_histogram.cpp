#include "color_histogram.h"
#include "color_normalizer.h"

#include <algorithm>

namespace omp2
{
	std::vector<color> get_ordered(std::vector<color> input_colors,
	                               const std::function<unsigned char(color)>& color_selector)
	{
		std::sort(
			std::begin(input_colors),
			std::end(input_colors),
			[color_selector](const color a, const color b)
			{
				return color_selector(a) < color_selector(b);
			});

		return input_colors;
	}

	color_histogram::color_histogram(
		std::vector<color> colors,
		const std::function<unsigned char(color)>& color_selector)
		: colors_(colors),
		  color_selector_(color_selector)
	{
		colors_ = get_ordered(colors, color_selector_);
	}

	unsigned char color_histogram::get_max_value()
	{
		const auto max_value_index = colors_.size() - colors_.size() / color_normalizer::ignore_percent;
		return color_selector_(colors_[max_value_index]);
	}

	unsigned char color_histogram::get_min_value()
	{
		const auto min_value_index = colors_.size() / color_normalizer::ignore_percent;
		return color_selector_(colors_[min_value_index]);
	}
}
