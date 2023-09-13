using System;


namespace R5T.S0102
{
    public class TypeOperator : ITypeOperator
    {
        #region Infrastructure

        public static ITypeOperator Instance { get; } = new TypeOperator();


        private TypeOperator()
        {
        }

        #endregion
    }
}
