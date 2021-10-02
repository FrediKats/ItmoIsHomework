#pragma once
#include <functional>
#include <vector>

#include "color.h"
#include "color_morphism.h"

class morphism_producer
{
public:
	morphism_producer();
	morphism_producer(
		const std::function<unsigned char(omp2::color)>& color_selector,
		std::vector<omp2::color> input_colors);

	color_morphism produce() const;

private:
	color_morphism value_;
};
