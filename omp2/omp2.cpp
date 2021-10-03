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

void common_run(const std::string& input_file_path, const std::string& output_file_path, const int thread_count)
{
	auto reader = omp2::color_image_reader(input_file_path);
	auto writer = omp2::color_image_writer(output_file_path);
	omp2::pnm_image_descriptor image_descriptor = reader.read();

	const int optimal_thread_count = omp_get_num_procs();
	auto multithread_color_normalizer = omp2::multithread_color_normalizer(thread_count == 0 ? optimal_thread_count : thread_count);
	auto singlethread_color_normalizer = omp2::single_thread_color_normalizer();

	omp2::color_normalizer* normalizer;
	if (thread_count >= 0)
		normalizer = &multithread_color_normalizer;
	else
		normalizer = &singlethread_color_normalizer;

	const auto start = std::chrono::system_clock::now();
	const auto result = normalizer->modify(image_descriptor.color);
	const auto end = std::chrono::system_clock::now();

	const std::chrono::duration<double> difference = end - start;

	writer.write(image_descriptor.update_colors(result));

	std::cout << "\nTime (" << thread_count << " thread(s)): " << difference.count() * 1000 << " ms" << std::endl;
}

int console_run(int argc, char** argv)
{
	try
	{
		//NB: exec_path input_file_path output_file_path thread_count
		if (argc == 3)
		{
			common_run(std::string(argv[1]), std::string(argv[2]), atoi(argv[3]));
			return 0;
		}
		else
		{
			std::cout << "Unexpected argument count";
			return 1;
		}
	}
	catch (std::exception& e)
	{
		std::cout << "Unexpected error.\n" << e.what();
		return 1;
	}
	catch (...)
	{
		std::cout << "Unexpected error.";
		return 1;
	}
}

int main(int argc, char** argv)
{
	return console_run(argc, argv);
}
