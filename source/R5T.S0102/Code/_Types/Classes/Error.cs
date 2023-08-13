using System;


namespace R5T.S0102.N001
{
    public class Error : IdentityName,
        IWithErrorMessage
    {
        public N002.IErrorMessage ErrorMessage { get; set; }


        public Error()
        {
            this.KindMarker = Instances.KindMarkers.Error;
        }
    }
}
