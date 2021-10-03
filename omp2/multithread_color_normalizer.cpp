#include "multithread_color_normalizer.h"

#include "color_histogram.h"
#include "color_morphism.h"

omp2::multithread_color_normalizer::multithread_color_normalizer(int parallel_thread_count)
	: parallel_thread_count_(parallel_thread_count)
{
	const auto red_selector = std::function<unsigned char(color)>([](const color c) { return c.red; });
	const auto green_selector = std::function<unsigned char(color)>([](const color c) { return c.green; });
	const auto blue_selector = std::function<unsigned char(color)>([](const color c) { return c.blue; });

	selectors_ = std::vector<std::function<unsigned char(color)>>
	{
		red_selector,
		green_selector,
		blue_selector
	};
}

std::vector<omp2::color> omp2::multithread_color_normalizer::modify(const std::vector<color>& input_colors)
{
	return modify_static(input_colors);
}

std::vector<omp2::color> omp2::multithread_color_normalizer::modify_static(const std::vector<color>& input_colors)
{
	auto result = std::vector<color>(input_colors.size());
	auto histograms = std::vector<color_histogram>(3);

#pragma omp parallel num_threads(parallel_thread_count_)
	{
#pragma omp for schedule(static)
		for (int i = 0; i < selectors_.size(); i++)
		{
			histograms[i] = color_histogram(input_colors, selectors_[i]);
		}
	}

	const auto morphism = color_morphism(histograms);

#pragma omp parallel num_threads(parallel_thread_count_)
	{
#pragma omp for schedule(static)
		for (int index = 0; index < input_colors.size(); index++)
		{
			const color original_color = input_colors[index];
			const auto changed_color = color(
				morphism.change_color(original_color.red),
				morphism.change_color(original_color.green),
				morphism.change_color(original_color.blue));

			result[index] = changed_color;
		}
	}

	return result;
}

std::vector<omp2::color> omp2::multithread_color_normalizer::modify_dynamic(const std::vector<color>& input_colors)
{
	auto result = std::vector<color>(input_colors.size());
	auto histograms = std::vector<color_histogram>(3);

#pragma omp parallel num_threads(parallel_thread_count_)
	{
#pragma omp for schedule(dynamic)
		for (int i = 0; i < selectors_.size(); i++)
		{
			histograms[i] = color_histogram(input_colors, selectors_[i]);
		}
	}

	const auto morphism = color_morphism(histograms);

#pragma omp parallel num_threads(parallel_thread_count_)
	{
#pragma omp for schedule(dynamic)
		for (int index = 0; index < input_colors.size(); index++)
		{
			const color original_color = input_colors[index];
			const auto changed_color = color(
				morphism.change_color(original_color.red),
				morphism.change_color(original_color.green),
				morphism.change_color(original_color.blue));

			result[index] = changed_color;
		}
	}

	return result;
}

std::vector<omp2::color> omp2::multithread_color_normalizer::modify_guid(const std::vector<color>& input_colors)
{
	auto result = std::vector<color>(input_colors.size());
	auto histograms = std::vector<color_histogram>(3);

#pragma omp parallel num_threads(parallel_thread_count_)
	{
#pragma omp for schedule(guided)
		for (int i = 0; i < selectors_.size(); i++)
		{
			histograms[i] = color_histogram(input_colors, selectors_[i]);
		}
	}

	const auto morphism = color_morphism(histograms);

#pragma omp parallel num_threads(parallel_thread_count_)
	{
#pragma omp for schedule(guided)
		for (int index = 0; index < input_colors.size(); index++)
		{
			const color original_color = input_colors[index];
			const auto changed_color = color(
				morphism.change_color(original_color.red),
				morphism.change_color(original_color.green),
				morphism.change_color(original_color.blue));

			result[index] = changed_color;
		}
	}

	return result;
}
