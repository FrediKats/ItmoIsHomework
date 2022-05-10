#pragma once
#include <algorithm>
#include <functional>
#include <vector>

namespace omp2
{
	class functional_extension
	{
	public:
		template <class TInput, class TOutput>
		static TOutput map_fold(std::vector<TInput> source,
		                 const std::function<TOutput(TInput)>& selector,
		                 const std::function<TInput(std::vector<TInput>)>& fold);
	};

	template <class TInput, class TOutput>
	TOutput functional_extension::map_fold(std::vector<TInput> source,
	                                       const std::function<TOutput(TInput)>& selector,
	                                       const std::function<TInput(std::vector<TInput>)>& fold)
	{
		std::sort(
			std::begin(source),
			std::end(source),
			[selector](const TInput a, const TInput b)
			{
				return selector(a) > selector(b);
			});

		return selector(fold(source));
	}
}
