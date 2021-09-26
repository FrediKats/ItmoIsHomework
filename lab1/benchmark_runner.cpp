#include "benchmark_runner.h"

lab1::benchmark_runner::benchmark_runner(const int run_count): run_count_(run_count)
{
}

std::chrono::duration<double> lab1::benchmark_runner::benchmark_run(const std::function<void()>& functor) const
{
	std::chrono::duration<double> result = std::chrono::duration<double>::zero();
	for (auto i = 0; i < run_count_; i++)
	{
		const auto start = std::chrono::high_resolution_clock::now();
		functor();
		const auto end = std::chrono::high_resolution_clock::now();

		result += (end - start);
	}

	return result / run_count_;
}
