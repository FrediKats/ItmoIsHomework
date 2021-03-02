#include <iostream>
#include <omp.h>

const int size = 100000;
long long longs[size];
int ints[size];

int main(int argc, char* argv[]) {
	long long originalSum = 0;
	long long longSum = 0;
	int intSum = 0;


	for (int i = 0; i < size; i++)
	{
		longs[i] = 1;
		ints[i] = 1;
		originalSum += 1;
	}

#pragma omp parallel
	{
		int index = omp_get_thread_num();
		int count = omp_get_num_threads();

		int start = size * index / count;
		int end = size * (index + 1) / count;


		for (int i = start; i < end; i++)
		{
			longSum += longs[i];
			intSum += ints[i];
		}
	}

	std::cout << originalSum << std::endl;
	std::cout << longSum << std::endl;
	std::cout << intSum << std::endl;
}