#include "color_image_reader.h"
#include "color_image_writer.h"
#include "color_normalizer.h"

int main()
{
	const auto source_file = "";
	const auto destination_file = "";

	auto reader = omp2::color_image_reader(source_file);
	auto writer = omp2::color_image_writer(destination_file);

	const omp2::pnm_image_descriptor image_descriptor = reader.read();
	auto color_normalizer = omp2::color_normalizer();
	auto modified_colors = color_normalizer.modify(image_descriptor.color);

	auto result = omp2::pnm_image_descriptor(
		image_descriptor.version, modified_colors, image_descriptor.width, image_descriptor.height, image_descriptor.max_value);
	writer.write(result);
}
