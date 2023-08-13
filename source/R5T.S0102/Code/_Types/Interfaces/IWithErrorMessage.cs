using System;

using R5T.S0102.N002;


namespace R5T.S0102.N001
{
    public interface IWithErrorMessage :
        IHasErrorMessage
    {
        new IErrorMessage ErrorMessage { get; set; }
    }
}
