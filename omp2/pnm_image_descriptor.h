#pragma once
#include <vector>

#include "color.h"

namespace omp2
{
	template <class TPixel>
	class pnm_image_descriptor
	{
	public:
		pnm_image_descriptor(int version, std::vector<TPixel> color, int width, int height, int max_value);
		pnm_image_descriptor update_colors(std::vector<TPixel> new_colors) const;

		const int version;
		const std::vector<TPixel> color;
		const int width;
		const int height;
		const int max_value;
	};

	template <class TPixel>
	omp2::pnm_image_descriptor<TPixel>::pnm_image_descriptor(
		const int version,
		std::vector<TPixel> color,
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

	template <class TPixel>
	omp2::pnm_image_descriptor<TPixel> omp2::pnm_image_descriptor<TPixel>::update_colors(
		std::vector<TPixel> new_colors) const
	{
		auto result = pnm_image_descriptor(version, std::move(new_colors), width, height, max_value);
		return result;
	}
}
