using System;


namespace R5T.S0102
{
    public class ParameterInfoOperator : IParameterInfoOperator
    {
        #region Infrastructure

        public static IParameterInfoOperator Instance { get; } = new ParameterInfoOperator();


        private ParameterInfoOperator()
        {
        }

        #endregion
    }
}
