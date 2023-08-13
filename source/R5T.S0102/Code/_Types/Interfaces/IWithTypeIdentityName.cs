using System;


namespace R5T.S0102.N001
{
    public interface IWithTypeIdentityName :
        IHasTypeIdentityName
    {
        new TypeIdentityName TypeIdentityName { get; set; }
    }
}
