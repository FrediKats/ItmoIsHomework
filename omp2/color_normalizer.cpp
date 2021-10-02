#include "color_normalizer.h"

#include "color_histogram.h"
#include "color_morphism.h"
#include "functional_extension.h"

namespace omp2
{
	std::vector<color> color_normalizer::modify(const std::vector<color>& input_colors)
	{
		const auto red_selector = std::function<unsigned char(color)>([](const color c) { return c.red; });
		const auto green_selector = std::function<unsigned char(color)>([](const color c) { return c.green; });
		const auto blue_selector = std::function<unsigned char(color)>([](const color c) { return c.blue; });

		auto red_histogram = color_histogram(input_colors, red_selector);
		auto green_histogram = color_histogram(input_colors, green_selector);
		auto blue_histogram = color_histogram(input_colors, blue_selector);

		auto total_max = std::max(
			red_histogram.get_max_value(),
			std::max(
				green_histogram.get_max_value(),
				blue_histogram.get_max_value()));

		auto total_min = std::min(
			red_histogram.get_min_value(),
			std::min(
				green_histogram.get_min_value(),
				blue_histogram.get_min_value()));

		auto total_morphism = color_morphism(total_min, total_max);

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
