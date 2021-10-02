#include "morphism_producer.h"

#include <functional>
#include <vector>

#include "color.h"
#include <algorithm>

#include "color_histogram.h"

morphism_producer::morphism_producer()
{
}

morphism_producer::morphism_producer(
	const std::function<unsigned char(omp2::color)>& color_selector,
	std::vector<omp2::color> input_colors)
{
	auto histogram = color_histogram(input_colors, color_selector);
	value_ = color_morphism(histogram.get_min_value(), histogram.get_max_value());
}

color_morphism morphism_producer::produce() const
{
	return value_;
}
