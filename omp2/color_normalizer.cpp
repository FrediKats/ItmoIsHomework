#include "color_normalizer.h"

#include "color_morphism.h"
#include "functional_extension.h"
#include "morphism_producer.h"


unsigned char change_color(
	const unsigned char original_color_value,
	const color_morphism morphism)
{
	const auto delta = morphism.get_delta();
	const auto coefficient = morphism.get_coefficient();

	if (coefficient == 0.0)
		return original_color_value;

	if (original_color_value < delta)
		return 0;
	const double result = static_cast<double>(original_color_value - delta) * coefficient;
	return result;
}

//TODO: fix namespaces
std::vector<omp2::color> omp2::color_normalizer::modify(const std::vector<color>& input_colors)
{
	const auto red_selector = std::function<unsigned char(color)>([](const color c) { return c.red; });
	const auto green_selector = std::function<unsigned char(color)>([](const color c) { return c.green; });
	const auto blue_selector = std::function<unsigned char(color)>([](const color c) { return c.blue; });

	const auto red_color_morphism = morphism_producer(red_selector, input_colors).produce();
	const auto green_color_morphism = morphism_producer(green_selector, input_colors).produce();
	const auto blue_color_morphism = morphism_producer(blue_selector, input_colors).produce();

	auto result = std::vector<color>(input_colors.size());

	for (size_t index = 0; index < input_colors.size(); index++)
	{
		const color original_color = input_colors[index];
		const auto changed_color = color(
			change_color(original_color.red, red_color_morphism),
			change_color(original_color.green, green_color_morphism),
			change_color(original_color.blue, blue_color_morphism));

		result[index] = changed_color;
	}

	return result;
}
