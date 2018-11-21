using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3
{
    public class TransportationMatrix
    {
        private readonly double[] _producers;
        private readonly double[] _consumers;
        private readonly double[][] _tariffs;
        private readonly double[][] _cargoes;
        private double[] _producerPotentials;
        private double[] _consumerPotentials;

        public IReadOnlyList<double[]> Plan => _cargoes.ToList();

        public TransportationMatrix(IEnumerable<double> producers, IEnumerable<double> consumers, IEnumerable<IEnumerable<double>> tariffs)
        {
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
            _tariffs = tariffs.Select(x => x.ToArray()).ToArray();
            _cargoes = tariffs.Select(x => x.Select(y => 0.0).ToArray()).ToArray(); //rewrite this
            
            var totalProd = _producers.Sum();
            var totalCons = _consumers.Sum();

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

            Solve();

            _tariffs.Zip(_producerPotentials, (x, y) => x.Append(y)).Append(_consumerPotentials).Dump();
            Console.WriteLine();
        }

        private void PrefillCargoes()
        {
            var producersCopy = (double[])_producers.Clone();
            var consumersCopy = (double[])_consumers.Clone();
            var tariffsCopy = _tariffs.CloneArray();

            while (!tariffsCopy.Select(x => x.All(double.IsPositiveInfinity)).All(x => x))
            {
                var (i, j) = tariffsCopy.IndexOfMin(new TariffComparer());

                //Console.WriteLine($"i = {i}\tj = {j}");
                //tariffsCopy.Dump<double>();
                //Console.WriteLine();

                var min = Math.Min(producersCopy[i], consumersCopy[j]);
                _cargoes[i][j] = min;
                producersCopy[i] -= min;
                consumersCopy[j] -= min;
                tariffsCopy[i][j] = double.PositiveInfinity;


                //producersCopy.Dump();
                //Console.WriteLine();
                //consumersCopy.Dump();
                //Console.WriteLine();
                //_cargoes.Dump<double>();
                //Console.WriteLine();
                //Console.WriteLine("~~~~~~~~~~~~~~~~~~");
            }
        }

        private void SetPotentials()
        {
            var equations = new List<List<double>>();

            for (int i = 0; i < _producerPotentials.Length; i++)
            for (int j = 0; j < _consumerPotentials.Length; j++)
            {
                if (_cargoes[i][j] > 0)
                {
                    var el = Enumerable.Repeat(0.0, _producerPotentials.Length + _consumerPotentials.Length) //create method
                        .Append(_tariffs[i][j]).ToList();
                    el[i] = 1;
                    el[_producerPotentials.Length + j] = 1;
                    equations.Add(el);
                }
            }

            if (equations.Count < _producers.Length + _consumers.Length - 1)
            {
                foreach (var coord in PotentialDetector.Detect(_cargoes))
                {
                    var el = Enumerable.Repeat(0.0, _producerPotentials.Length + _consumerPotentials.Length)
                        .Append(_tariffs[coord.i][coord.j]).ToList();
                    el[coord.i] = 1;
                    el[_producerPotentials.Length + coord.j] = 1;
                    equations.Add(el);
                }
            }

            var freeEl = Enumerable.Repeat(0.0, _producerPotentials.Length + _consumerPotentials.Length)
                .Append(1).ToList();
            freeEl[0] = 1;
            equations.Add(freeEl);

            //Console.WriteLine("<");
            //equations.Dump<double>();
            //Console.WriteLine(">");

            equations = equations.DiagonalForm();

            //Console.WriteLine("<");
            //equations.Dump<double>();
            //Console.WriteLine(">");

            _producerPotentials = equations.Take(_producers.Length).Select(x => x.Last()).ToArray();
            _consumerPotentials = equations.Skip(_producers.Length).Select(x => x.Last()).ToArray();
        }

        private void Solve()
        {
            PrefillCargoes();

            SetPotentials();
            bool isCompleted = CheckIfFinish();

            while (!isCompleted)
            {
                var selectedCell = SelectCell();

                double potentialDiff = _producerPotentials[selectedCell.i] + _consumerPotentials[selectedCell.j] - _tariffs[selectedCell.i][selectedCell.j];

                var selectedSecondCell = SelectSecondCell(selectedCell.i, selectedCell.j, potentialDiff);

                //Console.WriteLine("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
                //_cargoes.Dump<double>();
                //Console.WriteLine();
                //Console.WriteLine($"i1 = {selectedCell.i}, j1 = {selectedCell.j}");
                //Console.WriteLine($"i2 = {selectedSecondCell.i}, j2 = {selectedSecondCell.j}");

                _tariffs.Select((x, i) => x.Append(_producerPotentials[i])).Append(_consumerPotentials).Dump<double>();

                //TODO: Move to method
                _cargoes[selectedCell.i][selectedCell.j] += _cargoes[selectedSecondCell.i][selectedCell.j];
                _cargoes[selectedSecondCell.i][selectedCell.j] = 0;
                _cargoes[selectedCell.i][selectedSecondCell.j] -= _cargoes[selectedCell.i][selectedCell.j];
                _cargoes[selectedSecondCell.i][selectedSecondCell.j] += _cargoes[selectedCell.i][selectedCell.j];

                //Console.WriteLine();
                //_cargoes.Dump<double>();
                //Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");

                SetPotentials();

                isCompleted = CheckIfFinish();
            }
        }

        private (int i, int j) SelectCell()
        {
            for (int i = 0; i < _producerPotentials.Length; i++)
            for (int j = 0; j < _consumerPotentials.Length; j++)
                if (_tariffs[i][j] - _producerPotentials[i] - _consumerPotentials[j] < 0)
                {
                    return (i, j);
                }

            throw new NotImplementedException();
        }

        private (int i, int j) SelectSecondCell(int i1, int j1, double potentialDiff)
        {
            for (int i = 0; i < _producerPotentials.Length; i++)
            {
                for (int j = 0; j < _consumerPotentials.Length; j++)
                {
                    if (i == i1 || j == j1)
                        continue;

                    if (_cargoes[i][j] > 0 &&
                        potentialDiff.Equals(_tariffs[i1][j] - _tariffs[i1][j1] + _tariffs[i][j1] - _tariffs[i][j]))
                        return (i, j);
                }
            }

            throw new NotImplementedException();
        }

        private bool CheckIfFinish()
        {
            return _tariffs
                .Select((x, a) => x
                    .Select((y, b) => y - _producerPotentials[a] - _consumerPotentials[b])
                    .All(y => y >= 0))
                .All(x => x);
        }

        private class TariffComparer : IComparer<double>
        {
            public int Compare(double x, double y)
            {
                if (!double.IsPositiveInfinity(x) && y.Equals(0)) return -1;
                if (x.Equals(0) && !double.IsPositiveInfinity(y)) return 1;
                if (double.IsPositiveInfinity(x) && double.IsPositiveInfinity(y)) return 0;

                return Math.Sign(x - y);
            }
        }
    }
}
