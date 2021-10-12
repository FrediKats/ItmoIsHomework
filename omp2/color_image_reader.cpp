#define _CRT_SECURE_NO_WARNINGS

#include <fstream>
#include <iostream>
#include <string>
#include <utility>
#include <vector>

#include "color_image_reader.h"
#include "color.h"

omp2::color_image_reader::color_image_reader(std::string file_path): file_path_(std::move(file_path))
{
}

omp2::pnm_image_descriptor<omp2::color> omp2::color_image_reader::read() const
{
	FILE* file = fopen(file_path_.c_str(), "rb");
	unsigned char* pixel_data = nullptr;

	try
	{
		//NB: https://stackoverflow.com/a/36184106
		int version;
		int width;
		int height;
		int max_value;

		const auto read_argument_count = fscanf(file, "P%i\n%i %i\n%i\n", &version, &width, &height, &max_value);
		if (read_argument_count != 4)
			throw std::exception("Failed to read arguments from file");

		const size_t buffer_size = 3 * width * height;
		pixel_data = new unsigned char[buffer_size];

		const size_t pixel_count = fread(pixel_data, sizeof(char), buffer_size, file);
		if (pixel_count != buffer_size)
			throw std::exception("Failed to read pixels from file");

		auto colors = std::vector<color>(buffer_size / 3);

		for (size_t i = 0; i < buffer_size; i += 3) {
			unsigned char red = pixel_data[i];
			unsigned char green = pixel_data[i + 1];
			unsigned char blue = pixel_data[i + 2];
			colors[i / 3] = color(red, green, blue);
		}

		delete[] pixel_data;
		fclose(file);
		return pnm_image_descriptor<color>(version, colors, width, height, max_value);
	}
	catch (...)
	{
		delete[] pixel_data;
		fclose(file);
		throw;
	}
}
