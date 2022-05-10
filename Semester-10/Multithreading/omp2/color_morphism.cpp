#include "color_morphism.h"

#include "color_histogram.h"

namespace omp2
{
	color_morphism::color_morphism()
	{
	}

	color_morphism::color_morphism(const unsigned char min, const unsigned char max) : min_(min), max_(max)
	{
	}

	color_morphism::color_morphism(const std::vector<color_histogram>& histograms)
	{
		unsigned char min = histograms[0].get_min_value();
		unsigned char max = histograms[0].get_max_value();
		for (auto& histogram : histograms)
		{
			min = std::min(min, histogram.get_min_value());
			max = std::max(max, histogram.get_max_value());
		}

		min_ = min;
		max_ = max;
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
