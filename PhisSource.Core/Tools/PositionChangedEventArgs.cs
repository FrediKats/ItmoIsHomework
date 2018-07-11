using System;
using PhysicsSource.Core.Models;

namespace PhysicsSource.Core.Tools
{
    //toDO: rename to ... ParameterChanged?
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