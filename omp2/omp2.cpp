#include <iostream>
#include <sstream>

#include "color_reader.h"

int main()
{
	omp2::color_reader reader = omp2::color_reader("img");

	auto colors = reader.read();

    std::cout << "Hello World!\n";
}
