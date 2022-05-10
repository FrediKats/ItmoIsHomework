#pragma once
#include "matrix.h"

namespace lab1
{
	class fast_matrix : public matrix
	{
	public:
		fast_matrix(const std::vector<std::vector<float>>& data);
		fast_matrix(const matrix& data);

		float determinant() override;
	};

}

