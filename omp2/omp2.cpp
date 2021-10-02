#include <iostream>
#include <sstream>

#include "color_image_reader.h"
#include "color_image_writer.h"
#include "color_normalizer.h"

int main()
{
	const auto source_file = "";
	const auto destination_file = "";

	auto reader = omp2::color_image_reader(source_file);
	auto writer = omp2::color_image_writer(destination_file);
	//auto color_normalizer = omp2::color_normalizer();

	const omp2::pnm_image_descriptor image_descriptor = reader.read();
	writer.write(image_descriptor);
	////TODO: change type to double
	//auto modified_color = color_normalizer.modify(pnm_image_descriptor.color, 0, 1);
}
