#include "color_normalizer.h"

#include "functional_extension.h"

std::vector<omp2::color> omp2::color_normalizer::modify(std::vector<color> input_colors, int delta, int coefficient)
{
	const auto red_selector = std::function<unsigned char(color)>([](const color c) { return c.red; });
	const auto blue_selector = std::function<unsigned char(color)>([](const color c) { return c.blue; });
	const auto green_selector = std::function<unsigned char(color)>([](const color c) { return c.green; });

	auto result = std::vector<color>(input_colors.size());
	for (size_t index = 0; index < input_colors.size(); index++)
	{
		const auto changed_color = color(
			change_color(input_colors, index, delta, coefficient, red_selector),
			change_color(input_colors, index, delta, coefficient, green_selector),
			change_color(input_colors, index, delta, coefficient, blue_selector));
		input_colors[index] = changed_color;
	}

	return input_colors;
}

unsigned char omp2::color_normalizer::change_color(
	std::vector<color> input_colors,
	size_t index,
	int delta,
	int coefficient,
	const std::function<unsigned char(color)>& color_selector)
{
	auto minimal_value = functional_extension::map_fold(
		input_colors,
		color_selector,
		std::function<color(std::vector<color>)>([](std::vector<color> colors) { return colors[0]; }));

	auto max_value = functional_extension::map_fold(
		input_colors,
		color_selector,
		std::function<color(std::vector<color>)>([](std::vector<color> colors) { return colors[colors.size() - 1]; }));

	return (color_selector(input_colors[index]) + delta) * coefficient;
}
