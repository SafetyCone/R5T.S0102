using System;


namespace R5T.S0102
{
    public class Operator : IOperator
    {
        #region Infrastructure

        public static IOperator Instance { get; } = new Operator();


        private Operator()
        {
        }

        #endregion
    }
}
