#include "image_resizer.h"

omp2::pnm_image_descriptor image_resizer::resize(omp2::pnm_image_descriptor original, const int zoom_level)
{
	std::vector<omp2::color> resized = std::vector<omp2::color>(original.color.size() * zoom_level * zoom_level);

	auto w = original.width;
	auto h = original.height;
	for (size_t index = 0; index < resized.size(); index++)
	{
		resized[index] = original.color[(index / w % h) * w + index % w];
	}

	return omp2::pnm_image_descriptor(original.version, resized, w * zoom_level, h * zoom_level, original.max_value);
}
