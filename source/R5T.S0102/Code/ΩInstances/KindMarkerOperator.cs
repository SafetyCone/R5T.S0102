using System;


namespace R5T.S0102
{
    public class KindMarkerOperator : IKindMarkerOperator
    {
        #region Infrastructure

        public static IKindMarkerOperator Instance { get; } = new KindMarkerOperator();


        private KindMarkerOperator()
        {
        }

        #endregion
    }
}
