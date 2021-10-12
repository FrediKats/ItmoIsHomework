#pragma once
#include <functional>

#include "color_normalizer.h"

namespace omp2
{
	class multithread_color_normalizer : public color_normalizer
	{
	public:
		explicit multithread_color_normalizer(int parallel_thread_count);

		std::vector<color> modify(pnm_image_descriptor<omp2::color>) override;
		std::vector<color> modify_static(pnm_image_descriptor<omp2::color>);
		std::vector<color> modify_dynamic(pnm_image_descriptor<omp2::color>);
		std::vector<color> modify_guid(pnm_image_descriptor<omp2::color>);

	private:
		int parallel_thread_count_;
	};
}
