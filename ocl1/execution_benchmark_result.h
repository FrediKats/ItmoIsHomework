#pragma once
#include <chrono>

class execution_benchmark_result
{
public:
	execution_benchmark_result(
		const std::chrono::duration<double>& kernel_execution_time,
		const std::chrono::duration<double>& total_execution_time);

	execution_benchmark_result(
		std::chrono::time_point<std::chrono::steady_clock> total_start_time,
		std::chrono::time_point<std::chrono::steady_clock> kernel_start_time,
		std::chrono::time_point<std::chrono::steady_clock> kernel_finish_time,
		std::chrono::time_point<std::chrono::steady_clock> total_finish_time);

	std::chrono::duration<double> kernel_execution_time;
	std::chrono::duration<double> total_execution_time;
};
