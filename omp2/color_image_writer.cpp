#define _CRT_SECURE_NO_WARNINGS

#include "color_image_writer.h"

#include <utility>

omp2::color_image_writer::color_image_writer(std::string file_path) : file_path_(std::move(file_path))
{
}

void omp2::color_image_writer::write(const pnm_image_descriptor<color>& image) const
{
	FILE* file = fopen(file_path_.c_str(), "wb");
	unsigned char* pixel_data = nullptr;

	try
	{
		const int argument_count = fprintf(file, "P%i\n%i %i\n%i\n", image.version, image.width, image.height,
		                                   image.max_value);
		if (argument_count < 0)
			throw std::exception(("Failed to write arguments to file. Return: " + std::to_string(argument_count)).c_str());

		if (image.version == 6)
		{
			const size_t buffer_size = 3 * image.width * image.height;
			pixel_data = new unsigned char[buffer_size];

			for (size_t index = 0; index < image.color.size(); index++)
			{
				const auto current_pixel = image.color[index];
				pixel_data[index * 3] = current_pixel.red;
				pixel_data[index * 3 + 1] = current_pixel.green;
				pixel_data[index * 3 + 2] = current_pixel.blue;
			}

			const size_t pixel_count = fwrite(pixel_data, sizeof(char), buffer_size, file);
			if (pixel_count != buffer_size)
				throw std::exception("Failed to write pixels to file");

			delete[] pixel_data;
			fclose(file);
		}
		else
		{
			const size_t buffer_size = image.width * image.height;
			pixel_data = new unsigned char[buffer_size];

			for (size_t index = 0; index < image.width * image.height; index++)
			{
				const auto current_pixel = image.color[index];
				pixel_data[index] = current_pixel.red;
			}

			const size_t pixel_count = fwrite(pixel_data, sizeof(char), buffer_size, file);
			if (pixel_count != buffer_size)
				throw std::exception("Failed to write pixels to file");

			delete[] pixel_data;
			fclose(file);
		}


		
	}
	catch (...)
	{
		delete[] pixel_data;
		fclose(file);
		throw;
	}
}
