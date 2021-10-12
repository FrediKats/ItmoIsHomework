#pragma once
#include "pnm_image_descriptor.h"

class image_resizer
{
public:
	static omp2::pnm_image_descriptor<omp2::color> resize(omp2::pnm_image_descriptor<omp2::color> original, int zoom_level);
};
