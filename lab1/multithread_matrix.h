#pragma once
#include "matrix.h"

namespace lab1
{
	class multithread_matrix : public matrix
	{
	public:
		multithread_matrix(const std::vector<std::vector<float>>& data, int parallel_thread_count);
		multithread_matrix(const matrix& data, int parallel_thread_count);
		float determinant() override;

	private:
		const int parallel_thread_count_;
	};
}