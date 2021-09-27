#include <iostream>
#include <sstream>

#include "color_reader.h"
#include "color_normalizer.h"

int main()
{
	auto reader = omp2::color_reader("img");
	auto color_normalizer = omp2::color_normalizer();

	std::vector<omp2::color> colors = reader.read();
	//TODO: change type to double
	auto modified_color = color_normalizer.modify(colors, 0, 1);



    std::cout << "Hello World!\n";
}
