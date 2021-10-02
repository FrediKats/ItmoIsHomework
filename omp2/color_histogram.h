#pragma once
#include <functional>
#include <vector>

#include "color.h"

class color_histogram
{
public:
	color_histogram(std::vector<omp2::color> colors, const std::function<unsigned char(omp2::color)>& color_selector);

	unsigned char get_max_value();
	unsigned char get_min_value();

private:
	std::vector<omp2::color> colors_;
	const std::function<unsigned char(omp2::color)>& color_selector_;
};
