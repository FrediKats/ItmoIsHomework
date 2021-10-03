#pragma once
#include <vector>

namespace omp2
{
	class color_histogram;

	class color_morphism
	{
	public:
		color_morphism();
		color_morphism(unsigned char min, unsigned char max);
		color_morphism(const std::vector<color_histogram>& histograms);

		unsigned char change_color(unsigned char original_color_value) const;

	private:
		unsigned char min_;
		unsigned char max_;
	};
}
