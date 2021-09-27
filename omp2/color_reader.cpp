#include <fstream>
#include <iostream>
#include <sstream>
#include <string>
#include <vector>

#include "color_reader.h"
#include "color.h"

omp2::color_reader::color_reader(const std::string& file_path): file_path_(file_path)
{
}

omp2::image_descriptor omp2::color_reader::read() const
{
	std::ifstream file_descriptor(file_path_);
	if (!file_descriptor.good())
		throw std::exception("Can not open file");

	try
	{
		size_t size;
		file_descriptor >> size;
		std::vector<std::vector<float>> result(size);

		for (size_t row_index = 0; row_index < size; row_index++)
		{
			std::vector<float> temp(size);
			for (size_t column_index = 0; column_index < size; column_index++)
			{
				file_descriptor >> temp[column_index];
			}
			result[row_index] = temp;
		}


		//NB: https://stackoverflow.com/a/36184106
		std::string line;
		int width, height = 0;
		std::getline(file_descriptor, line);       //type of file, skip, it will always be for this code p6
		std::getline(file_descriptor, line);       // width and height of the image
		std::stringstream line_stream(line); //extract the width and height;
		line_stream >> width;
		line_stream >> height;

		int buffer_size = 3 * width * height;
		auto pixel_data = new char[buffer_size];
		file_descriptor.read(pixel_data, buffer_size);
		auto colors = std::vector<color>();

		for (int i = 0; i < buffer_size; i += 3) {
			unsigned char red = pixel_data[i];
			unsigned char green = pixel_data[i + 1];
			unsigned char blue = pixel_data[i + 2];
			colors.emplace_back(red, green, blue);
		}

		delete[] pixel_data;
		file_descriptor.close();

		return image_descriptor(colors, width, height);
	}
	catch (...)
	{
		file_descriptor.close();
		throw;
	}
}
