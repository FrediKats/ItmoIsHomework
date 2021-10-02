#include "color_normalizer.h"

#include "functional_extension.h"

unsigned char get_delta(const std::vector<omp2::color>& input_colors, const std::function<unsigned char(omp2::color)>& color_selector)
{
	const auto min_value_index = input_colors.size() / omp2::color_normalizer::ignore_percent;
	return color_selector(input_colors[min_value_index]);
}

double get_coefficient(
	const std::vector<omp2::color>& input_colors,
	const std::function<unsigned char(omp2::color)>& color_selector,
	const unsigned char max_value)
{
	const auto min_value_index = input_colors.size() / omp2::color_normalizer::ignore_percent;
	const auto max_value_index = input_colors.size() - input_colors.size() / omp2::color_normalizer::ignore_percent;
	const auto value_range = color_selector(input_colors[max_value_index]) - color_selector(input_colors[min_value_index]);
	if (value_range == 0)
		return 0;

	return max_value / (static_cast<double>(value_range));
}


std::vector<omp2::color> get_ordered(std::vector<omp2::color> input_colors, const std::function<unsigned char(omp2::color)>& color_selector)
{
	std::sort(
		std::begin(input_colors),
		std::end(input_colors),
		[color_selector](const omp2::color a, const omp2::color b)
		{
			return color_selector(a) < color_selector(b);
		});

	return input_colors;
}
unsigned char change_color(
	const std::vector<omp2::color>& original_colors,
	const std::vector<omp2::color>& ordered_colors,
	const size_t index,
	const std::function<unsigned char(omp2::color)>& color_selector)
{
	const auto delta = get_delta(ordered_colors, color_selector);
	//TODO: fix max value
	const auto coefficient = get_coefficient(ordered_colors, color_selector, 255);
	if (coefficient == 0.0)
		return color_selector(original_colors[index]);

	if (color_selector(original_colors[index]) < delta)
		return 0;
	const double result = static_cast<double>(color_selector(original_colors[index]) - delta) * coefficient;
	return result;
}

std::vector<omp2::color> omp2::color_normalizer::modify(
	const std::vector<color>& input_colors)
{
	const auto red_selector = std::function<unsigned char(color)>([](const color c) { return c.red; });
	const auto blue_selector = std::function<unsigned char(color)>([](const color c) { return c.blue; });
	const auto green_selector = std::function<unsigned char(color)>([](const color c) { return c.green; });

	auto ordered_red = get_ordered(input_colors, red_selector);
	auto ordered_green = get_ordered(input_colors, green_selector);
	auto ordered_blue = get_ordered(input_colors, blue_selector);

	auto result = std::vector<color>(input_colors.size());
	for (size_t index = 0; index < input_colors.size(); index++)
	{
		const auto changed_color = color(
			change_color(input_colors, ordered_red, index, red_selector),
			change_color(input_colors, ordered_green, index, green_selector),
			change_color(input_colors, ordered_blue, index, blue_selector));
		result[index] = changed_color;
	}

	return result;
}
