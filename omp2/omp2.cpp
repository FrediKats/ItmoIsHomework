// omp2.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <fstream>
#include <iostream>
#include <sstream>
#include <string>
#include <vector>

class color
{
public:
	unsigned char red;
	unsigned char green;
	unsigned char blue;
	
	color(const unsigned char red, const unsigned char green, const unsigned char blue) :red(red), green(green), blue(blue) {  }
};

std::vector<color> read(const std::string& file_path)
{
	std::ifstream file_descriptor(file_path);
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
		char* pixelData = new char[buffer_size];
		file_descriptor.read(pixelData, buffer_size);
		std::vector<color> colors = std::vector<color>();

		for (int i = 0; i < buffer_size; i += 3) {
			unsigned char red = pixelData[i];
			unsigned char green = pixelData[i + 1];
			unsigned char blue = pixelData[i + 2];
			colors.emplace_back(red, green, blue);
		}

		delete pixelData;
		file_descriptor.close();
		return colors;
	}
	catch (...)
	{
		file_descriptor.close();
		throw;
	}
}

int main()
{
    std::cout << "Hello World!\n";
}
