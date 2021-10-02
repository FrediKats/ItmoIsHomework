#include "color_morphism.h"

namespace omp2
{
	double calc_coefficient(
		const unsigned char min_value,
		const unsigned char max_value,
		const unsigned char limit_value)
	{
		const auto value_range = max_value - min_value;
		if (value_range == 0)
			return 0;

		return limit_value / (static_cast<double>(value_range));
	}

	color_morphism::color_morphism() : delta_(0), coefficient_(0)
	{
	}

	color_morphism::color_morphism(unsigned char min, unsigned char max)
	{
		delta_ = min;
		//TODO: fix max value
		coefficient_ = calc_coefficient(
			min,
			max,
			255);
	}

	unsigned char color_morphism::get_delta() const
	{
		return delta_;
	}

	double color_morphism::get_coefficient() const
	{
		return coefficient_;
	}
}
