using System;


namespace R5T.S0102
{
    public class MemberInfoOperator : IMemberInfoOperator
    {
        #region Infrastructure

        public static IMemberInfoOperator Instance { get; } = new MemberInfoOperator();


        private MemberInfoOperator()
        {
        }

        #endregion
    }
}
