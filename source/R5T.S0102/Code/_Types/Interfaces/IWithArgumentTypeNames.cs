using System;

using R5T.S0102.N002;


namespace R5T.S0102.N001
{
    public interface IWithArgumentTypeNames :
        IHasArgumentTypeNames
    {
        new ITypeName[] ArgumentTypeNames { get; set; }
    }
}
