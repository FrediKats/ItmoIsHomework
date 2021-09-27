#include "image_descriptor.h"

omp2::image_descriptor::image_descriptor(std::vector<omp2::color> color, const int width, const int height)
	: color(std::move(color)),
	  width(width),
	  height(height)
{
}
