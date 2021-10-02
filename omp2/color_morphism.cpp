#include "color_morphism.h"

color_morphism::color_morphism() : delta_(0), coefficient_(0)
{
}

color_morphism::color_morphism(
	const unsigned char delta,
	const double coefficient)
	: delta_(delta),
	  coefficient_(coefficient)
{
}

unsigned char color_morphism::get_delta() const
{
	return delta_;
}

double color_morphism::get_coefficient() const
{
	return coefficient_;
}
