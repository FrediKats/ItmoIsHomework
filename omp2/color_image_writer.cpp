#include "color_image_writer.h"

omp2::color_image_writer::color_image_writer(std::string file_path) : file_path_(file_path)
{
}

void omp2::color_image_writer::write(const image_descriptor image) const
{
	FILE* file = fopen(file_path_.c_str(), "wb");

	try
	{
		//NB: https://stackoverflow.com/a/36184106
		//TODO: type
		int p_type = 6;
		int max_value = 255;

		fprintf(file, "P%i\n%i %i\n%i\n", p_type, image.width, image.height, max_value);

		int buffer_size = 3 * image.width * image.height;
		auto pixel_data = new char[buffer_size];

		for (int index = 0; index < image.color.size(); index++)
		{
			auto current_pixel = image.color[index];
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
