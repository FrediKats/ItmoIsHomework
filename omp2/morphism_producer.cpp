#include "morphism_producer.h"

#include <functional>
#include <vector>

#include "color.h"
#include <algorithm>

#include "color_histogram.h"

double get_coefficient(
	unsigned char min_value,
	unsigned char max_value,
	const unsigned char limit_value)
{
	const auto value_range = max_value - min_value;
	if (value_range == 0)
		return 0;

	return limit_value / (static_cast<double>(value_range));
}




morphism_producer::morphism_producer()
{
}

morphism_producer::morphism_producer(
	const std::function<unsigned char(omp2::color)>& color_selector,
	std::vector<omp2::color> input_colors)
{
	auto histogram = color_histogram(input_colors, color_selector);
	const auto delta = histogram.get_min_value();
	//TODO: fix max value
	const auto coefficient = get_coefficient(
		histogram.get_min_value(),
		histogram.get_max_value(),
		255);

	value_ = color_morphism(delta, coefficient);
}

color_morphism morphism_producer::produce() const
{
	return value_;
}
