#pragma once
#include <chrono>
#include <string>
#include <vector>

class execution_benchmark_result
{
public:
	std::chrono::duration<double> kernel_execution_time;
	std::chrono::duration<double> total_execution_time;

	execution_benchmark_result(
		const std::chrono::duration<double>& kernel_execution_time,
		const std::chrono::duration<double>& total_execution_time);

	execution_benchmark_result(
		std::chrono::time_point<std::chrono::steady_clock> total_start_time,
		std::chrono::time_point<std::chrono::steady_clock> kernel_start_time,
		std::chrono::time_point<std::chrono::steady_clock> kernel_finish_time,
		std::chrono::time_point<std::chrono::steady_clock> total_finish_time);

	std::string to_string()
	{
		return std::to_string(kernel_execution_time.count() * 1000) + ";" + std::to_string(total_execution_time.count() * 1000);
	}

	static execution_benchmark_result average(std::vector<execution_benchmark_result> results)
	{
		std::chrono::duration<double> kernel_execution_time = std::chrono::duration<double>::zero();
		std::chrono::duration<double> total_execution_time = std::chrono::duration<double>::zero();

		for (execution_benchmark_result result : results)
		{
			kernel_execution_time += result.kernel_execution_time;
			total_execution_time += result.total_execution_time;
		}

		return execution_benchmark_result(kernel_execution_time / results.size(), total_execution_time / results.size());
	}
};
