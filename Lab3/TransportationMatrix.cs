using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Lab3
{
    public class TransportationMatrix
    {
        private readonly double[] _producers;
        private readonly double[] _consumers;
        private readonly double[][] _tariffs;
        private readonly double[][] _cargoes;
        private double[] _producersPotential;
        private double[] _consumersPotential;

        public IReadOnlyList<double[]> Plan => _cargoes.ToList();

        public TransportationMatrix(IEnumerable<double> producers, IEnumerable<double> consumers, IEnumerable<IEnumerable<double>> tariffs)
        {
            //check number of consumers/producers
            //double diff = producers.Sum() - consumers.Sum();

            //if (diff > 0)
            //{
            //    consumers = consumers.Append(diff);
            //    tariffs = tariffs.Select(x => x.Append(0));
            //}
            //else
            //{
            //    producers = producers.Append(diff);
            //    tariffs = tariffs.Append(new double[consumers.Count()]);
            //}

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
            _producersPotential = Enumerable.Repeat(0.0, _producers.Length).ToArray();
            _consumersPotential = Enumerable.Repeat(0.0, _consumers.Length).ToArray();

            Solve();

            _tariffs.Zip(_producersPotential, (x, y) => x.Append(y)).Append(_consumersPotential).Dump();
            Console.WriteLine();
        }

        private void PrefillCargoes()
        {
            var producersCopy = (double[])_producers.Clone();
            var consumersCopy = (double[])_consumers.Clone();
            var tariffsCopy = _tariffs.CloneArray();

            //_tariffs.Dump();
            //Console.WriteLine();

            while (producersCopy.Any(x => x > 0) && consumersCopy.Any(x => x > 0))
            {
                (int i, int  j) = tariffsCopy.IndexOfMin();
                var min = Math.Min(producersCopy[i], consumersCopy[j]);
                _cargoes[i][j] = min;
                producersCopy[i] -= min;
                consumersCopy[j] -= min;
                tariffsCopy[i][j] = double.PositiveInfinity;

                //Console.WriteLine(string.Join("\t", producersCopy));
                //Console.WriteLine();
                //Console.WriteLine(string.Join("\t", consumersCopy));
                //Console.WriteLine();

                //_cargoes.Dump();
                //Console.WriteLine("~~~~~~~~~~~~~~~~~~");
            }
        }

        private void SetPotentials()
        {
            var equations = new List<List<double>>();
            var maxTariff = 0.0;

            for (int i = 0; i < _producersPotential.Length; i++)
            {
                for (int j = 0; j < _consumersPotential.Length; j++)
                {
                    //if (_cargoes[i][j] > 0)
                    //{
                    //    _consumersPotential[j] = _tariffs[i][j] - _producersPotential[i];

                    //    for (int k = i + 1; k < _producersPotential.Length; k++)
                    //    {
                    //        if (_cargoes[k][j] > 0)
                    //        {
                    //            _producersPotential[k] = _tariffs[k][j] - _consumersPotential[j];
                    //        }
                    //    }
                    //}

                    maxTariff = Math.Max(maxTariff, _tariffs[i][j]);

                    if (_cargoes[i][j] > 0)
                    {
                        var el = Enumerable.Repeat(0.0, _producersPotential.Length + _consumersPotential.Length).Append(_tariffs[i][j]).ToList();
                        el[i] = 1;
                        el[_producersPotential.Length + j] = 1;
                        equations.Add(el);
                    }
                }
            }

            var count = equations.Count;

            for (int i = 0; i < _producersPotential.Length + _consumersPotential.Length - count; i++)
            {
                var el = Enumerable.Repeat(0.0, _producersPotential.Length + _consumersPotential.Length)
                                    .Append(maxTariff + i + 1).ToList();
                el[equations.Count] = 1;
                equations.Add(el);
            }

            equations = equations.DiagonalForm();

            _producersPotential = equations.Take(_producers.Length).Select(x => x.Last()).ToArray();
            _consumersPotential = equations.Skip(_producers.Length).Select(x => x.Last()).ToArray();
        }

        private void Solve()
        {
            PrefillCargoes();
            SetPotentials();

            bool isCompleted = _tariffs.Select((x, i) => x.Select((y, j) => y - _producersPotential[i] - _consumersPotential[j]).All(y => y >= 0)).All(x => x);

            while (!isCompleted)
            {
                int i1 = 0, j1 = 0, i2 = 0, j2 = 0;

                for (int i = 0; i < _producersPotential.Length; i++)
                    for (int j = 0; j < _consumersPotential.Length; j++)
                        if (_tariffs[i][j] - _producersPotential[i] - _consumersPotential[j] < 0)
                            (i1, j1) = (i, j);

                double potentialDiff = _producersPotential[i1] + _consumersPotential[j1] - _tariffs[i1][j1];

                for (int i = 0; i < _producersPotential.Length; i++)
                {
                    if (i != i1)
                    {
                        for (int j = 0; j < _consumersPotential.Length; j++)
                        {
                            if (j != j1 && _cargoes[i][j] > 0 &&
                                _tariffs[i1][j] - _tariffs[i1][j1] + _tariffs[i][j1] - _tariffs[i][j] ==
                                potentialDiff)
                                (i2, j2) = (i, j);
                        }
                    }
                }

                _cargoes[i1][j1] += _cargoes[i2][j1];
                _cargoes[i2][j1] = 0;
                _cargoes[i1][j2] -= _cargoes[i1][j1];
                _cargoes[i2][j2] += _cargoes[i1][j1];

                _cargoes.Dump();
                Console.WriteLine();
                SetPotentials();
                isCompleted = _tariffs.Select((x, a) => x.Select((y, b) => y - _producersPotential[a] - _consumersPotential[b]).All(y => y >= 0)).All(x => x);
                Console.WriteLine(string.Join(" ", _consumersPotential));
                Console.WriteLine(string.Join(" ", _producersPotential));
                _tariffs.Dump();
                Console.WriteLine();
            }
        }
    }
}
