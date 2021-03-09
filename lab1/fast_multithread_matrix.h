#pragma once
#include "fast_matrix.h"

namespace lab1
{
	class fast_multithread_matrix : public fast_matrix
	{
	public:
		fast_multithread_matrix(const std::vector<std::vector<float>>& data, int parallel_thread_count);
		fast_multithread_matrix(const matrix& data, int parallel_thread_count);

		float determinant() override;
		float determinant_static_schedule();
		float determinant_dynamic_schedule();
		float determinant_guided_schedule();

	private:
		const int parallel_thread_count_;
	};

}
