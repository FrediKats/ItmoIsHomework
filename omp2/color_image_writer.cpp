#define _CRT_SECURE_NO_WARNINGS

#include "color_image_writer.h"

#include <utility>

omp2::color_image_writer::color_image_writer(std::string file_path) : file_path_(std::move(file_path))
{
}

void omp2::color_image_writer::write(const pnm_image_descriptor& image) const
{
	FILE* file = fopen(file_path_.c_str(), "wb");

	try
	{
		fprintf(file, "P%i\n%i %i\n%i\n", image.version, image.width, image.height, image.max_value);

		const int buffer_size = 3 * image.width * image.height;
		const auto pixel_data = new char[buffer_size];

		for (int index = 0; index < image.color.size(); index++)
		{
			const auto current_pixel = image.color[index];
			pixel_data[index * 3] = current_pixel.red;
			pixel_data[index * 3 + 1] = current_pixel.green;
			pixel_data[index * 3 + 2] = current_pixel.blue;
		}

		fwrite(pixel_data, sizeof(char), buffer_size, file);
		fclose(file);
	}
	catch (...)
	{

		throw;
	}
}
