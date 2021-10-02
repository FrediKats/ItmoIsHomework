#include "color_histogram.h"

#include <algorithm>

#include "color_normalizer.h"

std::vector<omp2::color> get_ordered(std::vector<omp2::color> input_colors, const std::function<unsigned char(omp2::color)>& color_selector)
{
	std::sort(
		std::begin(input_colors),
		std::end(input_colors),
		[color_selector](const omp2::color a, const omp2::color b)
		{
			return color_selector(a) < color_selector(b);
		});

	return input_colors;
}

color_histogram::color_histogram(
	std::vector<omp2::color> colors,
	const std::function<unsigned char(omp2::color)>& color_selector)
	: colors_(colors),
	  color_selector_(color_selector)
{
	colors_ = get_ordered(colors, color_selector_);
}

unsigned char color_histogram::get_max_value()
{
	const auto max_value_index = colors_.size() - colors_.size() / omp2::color_normalizer::ignore_percent;
	return color_selector_(colors_[max_value_index]);
}

unsigned char color_histogram::get_min_value()
{
	const auto min_value_index = colors_.size() / omp2::color_normalizer::ignore_percent;
	return color_selector_(colors_[min_value_index]);
}
