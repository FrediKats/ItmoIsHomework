using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;

namespace Lab3
{
    public class TransportationMatrix
    {
        private readonly List<(int i, int j)> _basis;
        private readonly double[] _producers;
        private readonly double[] _consumers;
        private readonly double[][] _tariffs;
        private double[][] _cargoes;
        private double[] _producerPotentials;
        private double[] _consumerPotentials;

        public IReadOnlyList<double[]> Plan => _cargoes.ToList();

        public TransportationMatrix(IEnumerable<double> producers, IEnumerable<double> consumers, IEnumerable<IEnumerable<double>> tariffs)
        {
            //TODO: remove
            //  consumers
            //p
            //r
            //o
            //d
            //u
            //e
            //r
            //s

            _producers = producers.ToArray();
            _consumers = consumers.ToArray();
            //TODO: Add method IE<IE<T>> => T[][]
            _tariffs = tariffs.Select(x => x.ToArray()).ToArray();
            //TODO: Use generator
            _cargoes = tariffs.Select(x => x.Select(y => 0.0).ToArray()).ToArray();
            
            var totalProd = _producers.Sum();
            var totalCons = _consumers.Sum();

            //TODO: move to method
            if (totalProd > totalCons)
            {
                _tariffs = _tariffs.Select(x => x.Append(0).ToArray()).ToArray();
                _cargoes = _cargoes.Select(x => x.Append(0).ToArray()).ToArray();
                _consumers = _consumers.Append(totalProd - totalCons).ToArray();
            }
            else if (totalCons > totalProd)
            {
                _tariffs = _tariffs.Append(Enumerable.Repeat(0.0, _consumers.Length).ToArray()).ToArray();
                _cargoes = _cargoes.Append(Enumerable.Repeat(0.0, _consumers.Length).ToArray()).ToArray();
                _producers = _producers.Append(totalCons - totalProd).ToArray();
            }

            _producerPotentials = Enumerable.Repeat(0.0, _producers.Length).ToArray();
            _consumerPotentials = Enumerable.Repeat(0.0, _consumers.Length).ToArray();
            _basis = new List<(int, int)>(_consumers.Length + _producers.Length - 1);

            Solve();
        }

        private void PrefillCargoes()
        {
            //TODO: change signature to double[] Clone()
            var producersCopy = (double[])_producers.Clone();
            var consumersCopy = (double[])_consumers.Clone();
            var tariffsCopy = _tariffs.CloneArray();

            while (!tariffsCopy.Select(x => x.All(double.IsPositiveInfinity)).All(x => x))
            {
                (int i, int j) = tariffsCopy.IndexOfMin(x => x.Equals(0) ? double.MaxValue : x);

                var min = Math.Min(producersCopy[i], consumersCopy[j]);
                _cargoes[i][j] = min;
                producersCopy[i] -= min;
                consumersCopy[j] -= min;
                tariffsCopy[i][j] = double.PositiveInfinity;
            }
        }

        private void SetPotentials()
        {
            var equations = new List<List<double>>();

            foreach (var coord in _basis)
            {
                //TODO: create method
                var el = Enumerable.Repeat(0.0, _producerPotentials.Length + _consumerPotentials.Length)
                    .Append(_tariffs[coord.i][coord.j]).ToList();
                el[coord.i] = 1;
                el[_producerPotentials.Length + coord.j] = 1;
                equations.Add(el);
            }

            var freeEl = Enumerable.Repeat(0.0, _producerPotentials.Length + _consumerPotentials.Length)
                .Append(1)
                .ToList();

            freeEl[0] = 1;
            equations.Add(freeEl);

            equations = equations.DiagonalForm();

            _producerPotentials = equations.Take(_producers.Length).Select(x => x.Last()).ToArray();
            _consumerPotentials = equations.Skip(_producers.Length).Select(x => x.Last()).ToArray();
        }

        private void Solve()
        {
            PrefillCargoes();
            SetBasis();
            SetPotentials();

            bool isCompleted = CheckIfFinish();

            while (!isCompleted)
            {
                (int i, int j) selectedCell = _tariffs.Select((x, i) => x.Select((y, j) => y - _producerPotentials[i] - _consumerPotentials[j]))
                    .IndexOfMin(x => x);

                Recount(selectedCell);
                SetPotentials();
                isCompleted = CheckIfFinish();
            }
        }

        private void SetBasis()
        {
            for (int i = 0; i < _cargoes.Length; i++)
            for (int j = 0; j < _cargoes[0].Length; j++)
            {
                if (_cargoes[i][j] > 0)
                {
                    _basis.Add((i, j));
                }
            }

            if (_basis.Count < _consumers.Length + _producers.Length - 1)
            {
                _basis.AddRange(PotentialDetector.Detect(_cargoes));
            }
        }

        private void Recount((int i, int j) cell)
        {
            var cargoesSigns = Tools.CreateArray<int>(_cargoes.Length, _cargoes[0].Length);
            cargoesSigns[cell.i][cell.j] = 1;

            SearchInRow(cell.i);

            (int i, int j) minCargoCoord = cargoesSigns.Select((x, i) => x.Zip(_cargoes[i], (a, b) => a < 0 ? b : double.PositiveInfinity)).IndexOfMin(x => x);
            _cargoes = _cargoes.Select((x, i) => x.Zip(cargoesSigns[i], (cargo, sign) => cargo + sign * _cargoes[minCargoCoord.i][minCargoCoord.j]).ToArray())
                .ToArray();

            _basis[_basis.IndexOf(minCargoCoord)] = cell;

            bool SearchInRow(int row)
            {
                for (int i = 0; i < _cargoes[0].Length; i++)
                {
                    if (cargoesSigns[row][i].Equals(0) && _basis.Contains((row, i)))
                    {
                        cargoesSigns[row][i] = -1;

                        if (SearchInColumn(i))
                        {
                            return true;
                        }

                        cargoesSigns[row][i] = 0;
                    }
                }

                return false;
            }

            bool SearchInColumn(int column)
            {
                if (column == cell.j)
                {
                    return true;
                }

                for (int i = 0; i < _cargoes.Length; i++)
                {
                    if (cargoesSigns[i][column].Equals(0) && _basis.Contains((i, column)))
                    {
                        cargoesSigns[i][column] = 1;
                        

                        if (SearchInRow(i))
                        {
                            return true;
                        }

                        cargoesSigns[i][column] = 0;
                    }
                }

                return false;
            }
        }

        private bool CheckIfFinish()
        {
            return _tariffs
                .Select((x, a) => x
                    .Select((y, b) => y - _producerPotentials[a] - _consumerPotentials[b])
                    .All(y => y >= 0))
                .All(x => x);
        }
    }
}
