#include "color_morphism.h"

namespace omp2
{
	color_morphism::color_morphism()
	{
	}

	color_morphism::color_morphism(const unsigned char min, const unsigned char max) : min_(min), max_(max)
	{
	}

	unsigned char color_morphism::change_color(const unsigned char original_color_value) const
	{
		const auto value_range = max_ - min_;
		if (original_color_value < min_)
			return 0;

		const double coefficient = static_cast<double>(original_color_value - min_) / value_range;

		//TODO: fix max value
		double result = 255 * coefficient;
		return result > 255 ? 255 : result;
	}
}
