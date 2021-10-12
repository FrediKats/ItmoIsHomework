#include "single_thread_color_normalizer.h"

#include "color_histogram.h"
#include "color_morphism.h"

namespace omp2
{
	std::vector<color> single_thread_color_normalizer::modify(pnm_image_descriptor<omp2::color> image_descriptor)
	{
		auto input_colors = image_descriptor.color;
		auto selectors = image_descriptor.get_selectors();

		auto histograms = std::vector<color_histogram>(3);

		for (int i = 0; i < selectors.size(); i++)
		{
			histograms[i] = color_histogram(input_colors, selectors[i]);
		}

		const auto total_morphism = color_morphism(histograms);

		auto result = std::vector<color>(input_colors.size());

		for (size_t index = 0; index < input_colors.size(); index++)
		{
			const color original_color = input_colors[index];
			const auto changed_color = color(
				total_morphism.change_color(original_color.red),
				total_morphism.change_color(original_color.green),
				total_morphism.change_color(original_color.blue));

			result[index] = changed_color;
		}

		return result;
	}
	
}
