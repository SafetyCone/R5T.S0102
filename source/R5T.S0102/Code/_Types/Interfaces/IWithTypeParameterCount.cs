using System;


namespace R5T.S0102.N001
{
    public interface IWithTypeParameterCount : 
        IHasTypeParameterCount
    {
        new int TypeParameterCount { get; set; }
    }
}
