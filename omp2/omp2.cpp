#include <iostream>
#include <omp.h>
#include <ostream>

#include "color_image_reader.h"
#include "color_image_writer.h"
#include "color_normalizer.h"
#include "image_resizer.h"
#include "multithread_color_normalizer.h"
#include "single_thread_color_normalizer.h"
#include "../lab1/benchmark_runner.h"
#include "../lab1/benchmark_runner.cpp"

void benchmark_run(omp2::pnm_image_descriptor image_descriptor)
{
	lab1::benchmark_runner benchmark = lab1::benchmark_runner(10);

	auto color_normalizer = omp2::single_thread_color_normalizer();

	std::cout << "ThreadCount;Single;Dynamic;Static;Guided" << std::endl;
	for (int i = 1; i <= omp_get_num_procs(); i++)
	{
		auto mt_color_normalizer = omp2::multithread_color_normalizer(i);

		std::cout
			<< i << ";"
			<< benchmark.benchmark_run([&color_normalizer, &image_descriptor] { color_normalizer.modify(image_descriptor.color); }).count() * 1000 << ";"
			<< benchmark.benchmark_run([&mt_color_normalizer, &image_descriptor] { mt_color_normalizer.modify_dynamic(image_descriptor.color); }).count() * 1000 << ";"
			<< benchmark.benchmark_run([&mt_color_normalizer, &image_descriptor] { mt_color_normalizer.modify_static(image_descriptor.color); }).count() * 1000 << ";"
			<< benchmark.benchmark_run([&mt_color_normalizer, &image_descriptor] { mt_color_normalizer.modify_guid(image_descriptor.color); }).count() * 1000 << ";"
			<< std::endl;
	}
}

int main()
{
	const auto source_file = "";
	const auto destination_file = "";

	auto reader = omp2::color_image_reader(source_file);
	auto writer = omp2::color_image_writer(destination_file);

	omp2::pnm_image_descriptor image_descriptor = reader.read();
	auto color_normalizer = omp2::single_thread_color_normalizer();
	auto mt_color_normalizer = omp2::multithread_color_normalizer(4);

	//auto resizer = image_resizer();
	//auto resized = resizer.resize(image_descriptor, 4);
	//writer.write(resized);

	benchmark_run(image_descriptor);

	//auto modified_colors = color_normalizer.modify(image_descriptor.color);
	//modified_colors = mt_color_normalizer.modify(image_descriptor.color);

	//auto result = omp2::pnm_image_descriptor(
	//	image_descriptor.version, modified_colors, image_descriptor.width, image_descriptor.height, image_descriptor.max_value);
	//writer.write(result);
}
