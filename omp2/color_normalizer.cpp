#include "color_normalizer.h"

#include "functional_extension.h"

unsigned char get_delta(const std::vector<omp2::color>& input_colors, const std::function<unsigned char(omp2::color)>& color_selector)
{
	const auto min_value_index = input_colors.size() / omp2::color_normalizer::ignore_percent;
	return color_selector(input_colors[min_value_index]) * -1;
}

unsigned char get_coefficient(
	const std::vector<omp2::color>& input_colors,
	const std::function<unsigned char(omp2::color)>& color_selector,
	const unsigned char max_value)
{
	const auto min_value_index = input_colors.size() / omp2::color_normalizer::ignore_percent;
	const auto max_value_index = input_colors.size() - input_colors.size() / omp2::color_normalizer::ignore_percent;
	return max_value / (color_selector(input_colors[max_value_index]) - color_selector(input_colors[min_value_index]));
}

unsigned char change_color(
	const std::vector<omp2::color>& input_colors,
	const size_t index,
	const std::function<unsigned char(omp2::color)>& color_selector)
{
	const auto delta = get_delta(input_colors, color_selector);
	//TODO: fix max value
	const auto coefficient = get_coefficient(input_colors, color_selector, 256);

	return (color_selector(input_colors[index]) + delta) * coefficient;
}

std::vector<omp2::color> omp2::color_normalizer::modify(
	std::vector<color> input_colors)
{
	const auto red_selector = std::function<unsigned char(color)>([](const color c) { return c.red; });
	const auto blue_selector = std::function<unsigned char(color)>([](const color c) { return c.blue; });
	const auto green_selector = std::function<unsigned char(color)>([](const color c) { return c.green; });

	auto result = std::vector<color>(input_colors.size());
	for (size_t index = 0; index < input_colors.size(); index++)
	{
		const auto changed_color = color(
			change_color(input_colors, index, red_selector),
			change_color(input_colors, index, green_selector),
			change_color(input_colors, index, blue_selector));
		input_colors[index] = changed_color;
	}

	return input_colors;
}
