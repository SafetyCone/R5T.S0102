using System;

using R5T.T0142;
using R5T.T0162;


namespace R5T.S0102.N001
{
    [DataTypeMarker]
    public interface IHasKindMarker
    {
        IKindMarker KindMarker { get; }
    }
}
