using System;
using PhisSource.Core.Models;

namespace PhisSource.Core.Tools
{
    public class PositionChangedEventArgs : EventArgs
    {
        public PositionChangedEventArgs(TwoDimensional previousPosition, TwoDimensional currentPosition)
        {
            PreviousPosition = previousPosition;
            CurrentPosition = currentPosition;
        }

        public TwoDimensional PreviousPosition { get; }
        public TwoDimensional CurrentPosition { get; }

    }
}