#include "multithread_color_normalizer.h"

#include "color_histogram.h"
#include "color_morphism.h"

omp2::multithread_color_normalizer::multithread_color_normalizer(int parallel_thread_count) : parallel_thread_count_(parallel_thread_count)
{
}

std::vector<omp2::color> omp2::multithread_color_normalizer::modify(const std::vector<color>& input_colors)
{
	const auto red_selector = std::function<unsigned char(color)>([](const color c) { return c.red; });
	const auto green_selector = std::function<unsigned char(color)>([](const color c) { return c.green; });
	const auto blue_selector = std::function<unsigned char(color)>([](const color c) { return c.blue; });

	auto result = std::vector<color>(input_colors.size());

	const auto selectors = std::vector<std::function<unsigned char(color)>>
	{
		red_selector,
		green_selector,
		blue_selector
	};
	auto histograms = std::vector<color_histogram>(3);

#pragma omp parallel num_threads(parallel_thread_count_)
	{
#pragma omp for schedule(guided)
		for (int i = 0; i < selectors.size(); i++)
		{
			histograms[i] = color_histogram(input_colors, selectors[i]);
		}
	}

	unsigned char min = histograms[0].get_min_value();
	unsigned char max = histograms[0].get_max_value();
	for (auto& histogram : histograms)
	{
		min = std::min(min, histogram.get_min_value());
		max = std::max(max, histogram.get_max_value());
	}

	const auto total_morphism = color_morphism(min, max);

#pragma omp parallel num_threads(parallel_thread_count_)
	{
#pragma omp for schedule(guided)
		for (int index = 0; index < input_colors.size(); index++)
		{
			const color original_color = input_colors[index];
			const auto changed_color = color(
				total_morphism.change_color(original_color.red),
				total_morphism.change_color(original_color.green),
				total_morphism.change_color(original_color.blue));

			result[index] = changed_color;
		}
	}

	return result;
}
