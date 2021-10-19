#include "multithread_color_normalizer.h"

#include "color_histogram.h"
#include "color_morphism.h"

omp2::multithread_color_normalizer::multithread_color_normalizer(int parallel_thread_count)
	: parallel_thread_count_(parallel_thread_count)
{
	
}

std::vector<omp2::color> omp2::multithread_color_normalizer::modify(pnm_image_descriptor<omp2::color> image_descriptor)
{
	return modify_static(image_descriptor);
}

std::vector<omp2::color> omp2::multithread_color_normalizer::modify_static(pnm_image_descriptor<omp2::color> image_descriptor)
{
	auto input_colors = image_descriptor.color;
	auto selectors = image_descriptor.get_selectors();

	auto result = std::vector<color>(input_colors.size());
	auto histograms = std::vector<color_histogram>(3);

#pragma omp parallel num_threads(parallel_thread_count_)
	{
#pragma omp for schedule(static)
		for (int i = 0; i < selectors.size(); i++)
		{
			histograms[i] = color_histogram(input_colors, selectors[i]);
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

std::vector<omp2::color> omp2::multithread_color_normalizer::modify_dynamic(pnm_image_descriptor<omp2::color> image_descriptor)
{
	auto input_colors = image_descriptor.color;
	auto selectors = image_descriptor.get_selectors();

	auto result = std::vector<color>(input_colors.size());
	auto histograms = std::vector<color_histogram>(3);

#pragma omp parallel num_threads(parallel_thread_count_)
	{
#pragma omp for schedule(dynamic)
		for (int i = 0; i < selectors.size(); i++)
		{
			histograms[i] = color_histogram(input_colors, selectors[i]);
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

std::vector<omp2::color> omp2::multithread_color_normalizer::modify_guid(pnm_image_descriptor<omp2::color> image_descriptor)
{
	auto input_colors = image_descriptor.color;
	auto selectors = image_descriptor.get_selectors();

	auto result = std::vector<color>(input_colors.size());
	auto histograms = std::vector<color_histogram>(3);

#pragma omp parallel num_threads(parallel_thread_count_)
	{
#pragma omp for schedule(guided)
		for (int i = 0; i < selectors.size(); i++)
		{
			histograms[i] = color_histogram(input_colors, selectors[i]);
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
