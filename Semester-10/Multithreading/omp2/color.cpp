﻿#include "color.h"

omp2::color::color(const unsigned char red, const unsigned char green, const unsigned char blue):red(red), green(green), blue(blue)
{
}

omp2::color::color()
{
	red = 0;
	green = 0;
	blue = 0;
}
