#pragma once

#include "color.h"
#include <vector>

#include "pnm_image_descriptor.h"

namespace omp2
{
	class color_normalizer
	{
	public:
		virtual ~color_normalizer() = default;
		virtual std::vector<color> modify(pnm_image_descriptor<omp2::color>) = 0;
	};
}
