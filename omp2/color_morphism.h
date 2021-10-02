#pragma once

class color_morphism
{
public:
	color_morphism();
	color_morphism(unsigned char min, unsigned char max);

	unsigned char get_delta() const;
	double get_coefficient() const;

private:
	unsigned char delta_;
	double coefficient_;
};