using System;

namespace GeneticWay.Genetic.Tools
{
    public static class ExceptionTools
    {
        public static void ThrowIfNull(string argumentName, object argument)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }
    }
}