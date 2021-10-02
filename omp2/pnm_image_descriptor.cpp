#include "pnm_image_descriptor.h"

omp2::pnm_image_descriptor::pnm_image_descriptor(const int version,
                                                 std::vector<omp2::color> color,
                                                 const int width,
                                                 const int height,
                                                 const int max_value)
	: version(version),
	  color(std::move(color)),
	  width(width),
	  height(height),
	  max_value(max_value)
{
}
