#pragma once
#include <functional>

#include "color_normalizer.h"

namespace omp2
{
	class multithread_color_normalizer : color_normalizer
	{
	public:
		explicit multithread_color_normalizer(int parallel_thread_count);

		std::vector<color> modify(const std::vector<color>& input_colors) override;
		std::vector<color> modify_static(const std::vector<color>& input_colors);
		std::vector<color> modify_dynamic(const std::vector<color>& input_colors);
		std::vector<color> modify_guid(const std::vector<color>& input_colors);

	private:
		int parallel_thread_count_;
		std::vector<std::function<unsigned char(color)>> selectors_;
	};
}
