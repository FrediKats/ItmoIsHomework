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

omp2::pnm_image_descriptor omp2::color_image_reader::read() const
{
	FILE* file = fopen(file_path_.c_str(), "rb");

	try
	{
		//NB: https://stackoverflow.com/a/36184106
		int version;
		int width;
		int height;
		int max_value;

		fscanf(file, "P%i\n%i %i\n%i\n", &version, &width, &height, &max_value);

		const int buffer_size = 3 * width * height;
		const auto pixel_data = new char[buffer_size];

		fread(pixel_data, sizeof(char), buffer_size, file);

		//file_descriptor.read(pixel_data, buffer_size);
		auto colors = std::vector<color>();

		for (int i = 0; i < buffer_size; i += 3) {
			unsigned char red = pixel_data[i];
			unsigned char green = pixel_data[i + 1];
			unsigned char blue = pixel_data[i + 2];
			colors.emplace_back(red, green, blue);
		}

		delete[] pixel_data;
		//file_descriptor.close();
		fclose(file);
		return pnm_image_descriptor(version, colors, width, height, max_value);
	}
	catch (...)
	{
		//file_descriptor.close();
		throw;
	}
}
