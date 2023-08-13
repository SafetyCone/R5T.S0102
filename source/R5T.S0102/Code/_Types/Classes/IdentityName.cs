using System;

using R5T.T0162;


// Place in a separate namespace for ease of later extraction.
namespace R5T.S0102.N001
{
    /// <summary>
    /// A structural identity name type.
    /// Similar types:
    ///     * R5T.T0162.IIdentityName - String-based strong-type.
    /// </summary>
    public abstract class IdentityName :
        // Identity names have a kind marker, but that's it!
        IWithKindMarker
        // Structural identity names do not have a value, unlike the strongly-typed identity names.
        // Invidtual structural identity name types have their own types for the value.
    {
        public IKindMarker KindMarker { get; set; }


        public override string ToString()
        {
            var representation = Instances.IdentityNameOperator_N001.Get_InternalIdentityName(this).Value;
            return representation;
        }
    }
}
