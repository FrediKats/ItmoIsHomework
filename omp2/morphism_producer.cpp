#include "morphism_producer.h"

#include <functional>
#include <vector>

#include "color.h"
#include "color_normalizer.h"
#include <algorithm>

unsigned char get_delta(const std::vector<omp2::color>& input_colors, const std::function<unsigned char(omp2::color)>& color_selector)
{
	const auto min_value_index = input_colors.size() / omp2::color_normalizer::ignore_percent;
	return color_selector(input_colors[min_value_index]);
}

double get_coefficient(
	const std::vector<omp2::color>& input_colors,
	const std::function<unsigned char(omp2::color)>& color_selector,
	const unsigned char max_value)
{
	const auto min_value_index = input_colors.size() / omp2::color_normalizer::ignore_percent;
	const auto max_value_index = input_colors.size() - input_colors.size() / omp2::color_normalizer::ignore_percent;
	const auto value_range = color_selector(input_colors[max_value_index]) - color_selector(input_colors[min_value_index]);
	if (value_range == 0)
		return 0;

	return max_value / (static_cast<double>(value_range));
}


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

morphism_producer::morphism_producer()
{
}

morphism_producer::morphism_producer(
	const std::function<unsigned char(omp2::color)>& color_selector,
	std::vector<omp2::color> input_colors)
{
	std::vector<omp2::color> ordered_colors = std::vector<omp2::color>(input_colors);
	ordered_colors = get_ordered(ordered_colors, color_selector);

	const auto delta = get_delta(ordered_colors, color_selector);
	//TODO: fix max value
	const auto coefficient = get_coefficient(ordered_colors, color_selector, 255);

	value_ = color_morphism(delta, coefficient);
}

color_morphism morphism_producer::produce() const
{
	return value_;
}
