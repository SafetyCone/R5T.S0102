using System;

using R5T.L0062.T000;
using R5T.T0142;


namespace R5T.S0102.N001
{
    /// <summary>
    /// Used for validation of identity string from-signature generation functionality using from-member generation functionality as a reference.
    /// </summary>
    [DataTypeMarker]
    public class IdentityStringPair
    {
        public IIdentityString Reference { get; set; }
        public IIdentityString FromStructure { get; set; }
    }
}
