#include "color_histogram.h"
#include "color_normalizer.h"

#include <algorithm>

namespace omp2
{
	color_histogram::color_histogram(
		std::vector<color> colors,
		const std::function<unsigned char(color)>& color_selector)
		: colors_(colors),
		  color_selector_(color_selector)
	{
		const auto max_value_index = colors.size() - colors.size() / ignore_percent;
		const auto min_value_index = colors.size() / ignore_percent;

		auto nth_iterator = colors.begin() + max_value_index;
		std::nth_element(colors.begin(), nth_iterator, colors.end(), [color_selector](const color a, const color b)
		{
			return color_selector(a) < color_selector(b);
		});

		nth_iterator = std::begin(colors) + min_value_index;
		std::nth_element(colors.begin(), nth_iterator, colors.end(), [color_selector](const color a, const color b)
		{
			return color_selector(a) < color_selector(b);
		});

		min_ = color_selector_(colors[min_value_index]);
		max_ = color_selector_(colors[max_value_index]);
	}

	unsigned char color_histogram::get_max_value() const
	{
		return max_;
	}

	unsigned char color_histogram::get_min_value() const
	{
		return min_;
	}
}
