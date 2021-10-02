#pragma once
#include "pnm_image_descriptor.h"

class image_resizer
{
public:
	static omp2::pnm_image_descriptor resize(omp2::pnm_image_descriptor original, int zoom_level);
};
