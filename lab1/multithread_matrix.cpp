#include "multithread_matrix.h"

#include <omp.h>

namespace lab1
{
	multithread_matrix::multithread_matrix(const std::vector<std::vector<float>>& data,
	                                       const int parallel_thread_count)
		: matrix(data), parallel_thread_count_(parallel_thread_count)
	{
	}

	multithread_matrix::multithread_matrix(const matrix& data,
	                                       const int parallel_thread_count)
		: matrix(data), parallel_thread_count_(parallel_thread_count)
	{
	}

	float multithread_matrix::determinant()
	{
		return determinant_guided_schedule();
	}

	float multithread_matrix::determinant_static_schedule()
	{
		if (data_.size() == 1)
			return data_[0][0];

		float result = 0;
#pragma omp parallel num_threads(parallel_thread_count_)
		{
#pragma omp for schedule(static)
			for (int row_index = 0; row_index < data_.size(); row_index++)
			{
				const auto row_result = eval_determinant_internal(row_index);
#pragma omp atomic
				result += row_result;
			}
		}

		return result;
	}

	float multithread_matrix::determinant_dynamic_schedule()
	{
		if (data_.size() == 1)
			return data_[0][0];

		float result = 0;
#pragma omp parallel num_threads(parallel_thread_count_)
		{
#pragma omp for schedule(dynamic)
			for (int row_index = 0; row_index < data_.size(); row_index++)
			{
				const auto row_result = eval_determinant_internal(row_index);
#pragma omp atomic
				result += row_result;
			}
		}

		return result;
	}

	float multithread_matrix::determinant_guided_schedule()
	{
		if (data_.size() == 1)
			return data_[0][0];

		float result = 0;
#pragma omp parallel num_threads(parallel_thread_count_)
		{
#pragma omp for schedule(guided)
			for (int row_index = 0; row_index < data_.size(); row_index++)
			{
				const auto row_result = eval_determinant_internal(row_index);
#pragma omp atomic
				result += row_result;
			}
		}

		return result;
	}

	float multithread_matrix::eval_determinant_internal(int row_index)
	{
		float row_result = 0;
		for (size_t column_index = 0; column_index < data_[row_index].size(); column_index++)
		{
			const float tmp =
				(row_index + column_index % 2 ? 1.0f : -1.0f)
				* data_[row_index][column_index]
				* multithread_matrix(get_minor_as_vector(column_index, row_index), parallel_thread_count_).
				determinant();

			row_result += tmp;
		}

		return row_result;
	}
}
