#include "fast_matrix.h"

lab1::fast_matrix::fast_matrix(const std::vector<std::vector<float>>& data) : matrix(data)
{
}

lab1::fast_matrix::fast_matrix(const matrix& data) : matrix(data)
{
}

float lab1::fast_matrix::determinant()
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
        for (int j = i + 1; j < n; ++j)
            a[i][j] /= a[i][i];
        for (int j = 0; j < n; ++j)
            if (j != i && abs(a[j][i]) > EPS)
                for (int k = i + 1; k < n; ++k)
                    a[j][k] -= a[i][k] * a[j][i];
    }
    return det;
}