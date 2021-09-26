#pragma once
#include <chrono>
#include <functional>

namespace lab1
{
	class benchmark_runner
	{
	public:
		explicit benchmark_runner(int run_count);
		std::chrono::duration<double> benchmark_run(const std::function<void()>& functor) const;

	private:
		const int run_count_;
	};
}