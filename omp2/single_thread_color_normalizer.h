#pragma once
#include "color_normalizer.h"

namespace omp2
{
	class single_thread_color_normalizer : color_normalizer
	{
	public:
		std::vector<color> modify(const std::vector<color>& input_colors) override;

	};
}