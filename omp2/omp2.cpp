#include <iostream>
#include <sstream>

#include "color_reader.h"
#include "color_normalizer.h"

int main()
{
	auto reader = omp2::color_reader("img");
	auto color_normalizer = omp2::color_normalizer();

	const omp2::image_descriptor image_descriptor = reader.read();
	//TODO: change type to double
	auto modified_color = color_normalizer.modify(image_descriptor.color, 0, 1);



    std::cout << "Hello World!\n";
}
