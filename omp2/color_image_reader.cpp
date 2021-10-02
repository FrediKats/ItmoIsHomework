#define _CRT_SECURE_NO_WARNINGS

#include <fstream>
#include <iostream>
#include <sstream>
#include <string>
#include <vector>

#include "color_image_reader.h"
#include "color.h"

omp2::color_image_reader::color_image_reader(std::string file_path)
{
	file_path_ = file_path;
}

omp2::image_descriptor omp2::color_image_reader::read() const
{
	//std::ifstream file_descriptor(file_path_);
	//if (!file_descriptor.good())
	//	throw std::exception("Can not open file");

	FILE* file = fopen(file_path_.c_str(), "rb");

	try
	{
		/*size_t size;
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
		}*/


		//NB: https://stackoverflow.com/a/36184106
		int p_type;
		int width, height = 0, max_value;

		fscanf(file, "P%i\n%i %i\n%i\n", &p_type, &width, &height, &max_value);

		//file_descriptor >> p6;
		//file_descriptor >> width;
		//file_descriptor >> height;
		//file_descriptor >> max_value;

		int buffer_size = 3 * width * height;
		auto pixel_data = new char[buffer_size];

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
		return image_descriptor(colors, width, height);
	}
	catch (...)
	{
		//file_descriptor.close();
		throw;
	}
}