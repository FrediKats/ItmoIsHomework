#include <iostream>
#include <string>

#include "matrix.h"
#include "random_matrix_provider.h"

int main()
{
	auto matrix = lab1::random_matrix_provider::generate(4);

	std::cout << matrix.to_string();
	std::cout << "Hello World!\n";
}
