using System;

using R5T.T0162;


namespace R5T.S0102.N001
{
    public interface IWithKindMarker :
        IHasKindMarker
    {
        new IKindMarker KindMarker { get; set; }
    }
}