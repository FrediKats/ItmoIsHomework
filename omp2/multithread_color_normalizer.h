#pragma once
#include "color_normalizer.h"

namespace omp2
{
	class multithread_color_normalizer : color_normalizer
	{
	public:
		explicit multithread_color_normalizer(int parallel_thread_count);

		std::vector<color> modify(const std::vector<color>& input_colors) override;

	private:
		int parallel_thread_count_;
	};
}
