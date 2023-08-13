using System;

using R5T.T0178;
using R5T.T0179;


namespace R5T.S0102.N002
{
    /// <summary>
    /// Strongly-types a string as an arguments list.
    /// Note: does <strong>not</strong> include the beginning and ending parentheses.
    /// </summary>
    [StrongTypeMarker]
    public interface IArgumentList : IStrongTypeMarker,
        ITyped<string>
    {
    }
}