#include "execution_benchmark_result.h"

execution_benchmark_result::execution_benchmark_result(
	const std::chrono::duration<double>& kernel_execution_time,
	const std::chrono::duration<double>& total_execution_time):
	kernel_execution_time(kernel_execution_time),
	total_execution_time(total_execution_time)
{
}

execution_benchmark_result::execution_benchmark_result(
	const std::chrono::time_point<std::chrono::steady_clock> total_start_time,
	const std::chrono::time_point<std::chrono::steady_clock> kernel_start_time,
	const std::chrono::time_point<std::chrono::steady_clock> kernel_finish_time,
	const std::chrono::time_point<std::chrono::steady_clock> total_finish_time):
	kernel_execution_time(kernel_finish_time - kernel_start_time),
	total_execution_time(total_finish_time - total_start_time)
{
}
