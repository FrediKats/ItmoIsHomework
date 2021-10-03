#include "pnm_image_descriptor.h"

#include <utility>

omp2::pnm_image_descriptor::pnm_image_descriptor(
	const int version,
	std::vector<omp2::color> color,
	const int width,
	const int height,
	const int max_value)
	: version(version),
	  color(std::move(color)),
	  width(width),
	  height(height),
	  max_value(max_value)
{
}

omp2::pnm_image_descriptor omp2::pnm_image_descriptor::update_colors(std::vector<omp2::color> new_colors) const
{
	auto result = pnm_image_descriptor(version, std::move(new_colors), width, height, max_value);
	return result;
}
