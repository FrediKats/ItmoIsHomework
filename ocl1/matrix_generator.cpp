#include "matrix_generator.h"

matrix matrix_generator::generate(const size_t size)
{
	std::vector<std::vector<float>> result(size);
	for (size_t i = 0; i < size; i++)
	{
		std::vector<float> temp(size);
		for (size_t j = 0; j < size; j++)
		{
			//NB: https://stackoverflow.com/a/686373
			temp[j] = static_cast <float> (rand()) / static_cast <float> (RAND_MAX);
		}
		result[i] = temp;
	}
	return matrix(result);
}
