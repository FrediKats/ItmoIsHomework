#pragma once
#include <vector>
#include <algorithm>
#include <functional>

#include "color.h"

namespace omp2
{
	template <class TPixel>
	class pnm_image_descriptor
	{
	public:
		pnm_image_descriptor(int version, std::vector<TPixel> color, int width, int height, int max_value);
		pnm_image_descriptor update_colors(std::vector<TPixel> new_colors) const;

		std::vector<std::function<unsigned char(color)>> get_selectors();


		const int version;
		const std::vector<TPixel> color;
		const int width;
		const int height;
		const int max_value;

	private:
		std::vector<std::function<unsigned char(TPixel)>> selectors_;
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
		if (version == 6)
		{
			const auto red_selector = std::function<unsigned char(TPixel)>([](const TPixel& c) { return c.red; });
			const auto green_selector = std::function<unsigned char(TPixel)>([](const TPixel& c) { return c.green; });
			const auto blue_selector = std::function<unsigned char(TPixel)>([](const TPixel& c) { return c.blue; });

			selectors_ = std::vector<std::function<unsigned char(TPixel)>>
			{
				red_selector,
				green_selector,
				blue_selector
			};
		}
		else
		{
			const auto red_selector = std::function<unsigned char(TPixel)>([](const TPixel& c) { return c.red; });

			selectors_ = std::vector<std::function<unsigned char(TPixel)>>
			{
				red_selector,
			};
		}
	}

	template <class TPixel>
	omp2::pnm_image_descriptor<TPixel> omp2::pnm_image_descriptor<TPixel>::update_colors(
		std::vector<TPixel> new_colors) const
	{
		auto result = pnm_image_descriptor(version, std::move(new_colors), width, height, max_value);
		return result;
	}

	template <class TPixel>
	std::vector<std::function<unsigned char(color)>> pnm_image_descriptor<TPixel>::get_selectors()
	{
		return selectors_;
	}
}
