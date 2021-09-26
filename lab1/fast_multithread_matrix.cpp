#include "fast_multithread_matrix.h"
#include "aligned_allocator.h"

lab1::fast_multithread_matrix::fast_multithread_matrix(
    const std::vector<std::vector<float>>& data,
	int parallel_thread_count) : fast_matrix(data), parallel_thread_count_(parallel_thread_count)
{
}

lab1::fast_multithread_matrix::fast_multithread_matrix(const matrix& data, int parallel_thread_count)
    : fast_matrix(data), parallel_thread_count_(parallel_thread_count)
{
}

float lab1::fast_multithread_matrix::determinant()
{
    return determinant_static_schedule();
}

//float lab1::fast_multithread_matrix::determinant_static_schedule_with_align()
//{
//    const double EPS = 1E-9;
//    int n = data_.size();
//	
//    std::vector <std::vector<float, aligned_allocator<float>> > a;
//    a.reserve(data_.size());
//    for (const auto& datarow : data_)
//    {
//        std::vector<float, aligned_allocator<float>> row;
//        row.reserve(data_.size());
//        for (const auto& e : datarow)
//            row.push_back(e);
//        a.push_back(std::move(row));
//    }
//	
//    float det = 1;
//    for (int i = 0; i < n; ++i) {
//        int k = i;
//        for (int j = i + 1; j < n; ++j)
//            if (abs(a[j][i]) > abs(a[k][i]))
//                k = j;
//        if (abs(a[k][i]) < EPS) {
//            det = 0;
//            break;
//        }
//        std::swap(a[i], a[k]);
//        if (i != k)
//            det = -det;
//        det *= a[i][i];
//
//#pragma omp parallel num_threads(parallel_thread_count_)
//        {
//#pragma omp for schedule(static)
//                for (int j = i + 1; j < n; ++j)
//                    a[i][j] /= a[i][i];
//
//#pragma omp for schedule(static)
//                for (int j = 0; j < n; ++j)
//                    if (j != i && abs(a[j][i]) > EPS)
//                        for (int k = i + 1; k < n; ++k)
//                            a[j][k] -= a[i][k] * a[j][i];
//        }
//    }
//    return det;
//}

float lab1::fast_multithread_matrix::determinant_static_schedule()
{
    const double EPS = 1E-9;
    int n = data_.size();
    std::vector <std::vector<float> > a = data_;

    float det = 1;
    for (int i = 0; i < n; ++i) {
        int k = i;
        for (int j = i + 1; j < n; ++j)
            if (abs(a[j][i]) > abs(a[k][i]))
                k = j;
        if (abs(a[k][i]) < EPS) {
            det = 0;
            break;
        }
        std::swap(a[i], a[k]);
        if (i != k)
            det = -det;
        det *= a[i][i];

#pragma omp parallel num_threads(parallel_thread_count_)
        {
#pragma omp for schedule(static)
            for (int j = i + 1; j < n; ++j)
                a[i][j] /= a[i][i];

#pragma omp for schedule(static)
            for (int j = 0; j < n; ++j)
                if (j != i && abs(a[j][i]) > EPS)
                    for (int k = i + 1; k < n; ++k)
                        a[j][k] -= a[i][k] * a[j][i];
        }
    }
    return det;
}

float lab1::fast_multithread_matrix::determinant_dynamic_schedule()
{
    const double EPS = 1E-9;
    int n = data_.size();
    std::vector <std::vector<float> > a = data_;

    float det = 1;
    for (int i = 0; i < n; ++i) {
        int k = i;
        for (int j = i + 1; j < n; ++j)
            if (abs(a[j][i]) > abs(a[k][i]))
                k = j;
        if (abs(a[k][i]) < EPS) {
            det = 0;
            break;
        }
        std::swap(a[i], a[k]);
        if (i != k)
            det = -det;
        det *= a[i][i];

#pragma omp parallel num_threads(parallel_thread_count_)
        {
#pragma omp for schedule(dynamic)
            for (int j = i + 1; j < n; ++j)
                a[i][j] /= a[i][i];
        }

        for (int j = 0; j < n; ++j)
            if (j != i && abs(a[j][i]) > EPS)
                for (int k = i + 1; k < n; ++k)
                    a[j][k] -= a[i][k] * a[j][i];
    }
    return det;
}

float lab1::fast_multithread_matrix::determinant_guided_schedule()
{
    const double EPS = 1E-9;
    int n = data_.size();
    std::vector <std::vector<float> > a = data_;

    float det = 1;
    for (int i = 0; i < n; ++i) {
        int k = i;
        for (int j = i + 1; j < n; ++j)
            if (abs(a[j][i]) > abs(a[k][i]))
                k = j;
        if (abs(a[k][i]) < EPS) {
            det = 0;
            break;
        }
        std::swap(a[i], a[k]);
        if (i != k)
            det = -det;
        det *= a[i][i];

#pragma omp parallel num_threads(parallel_thread_count_)
        {
#pragma omp for schedule(guided)
            for (int j = i + 1; j < n; ++j)
                a[i][j] /= a[i][i];
        }

        for (int j = 0; j < n; ++j)
            if (j != i && abs(a[j][i]) > EPS)
                for (int k = i + 1; k < n; ++k)
                    a[j][k] -= a[i][k] * a[j][i];
    }
    return det;
}
