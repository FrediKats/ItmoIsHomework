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
		if (data_.size() == 1)
			return data_[0][0];

		float result = 0;
		int count = data_.size();
#pragma omp parallel num_threads(parallel_thread_count_)
		{
#pragma omp for schedule(guided)
			for (int row_index = 0; row_index < count; row_index++)
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
#pragma omp atomic
				result += row_result;
			}
		}


		return result;
	}
}
