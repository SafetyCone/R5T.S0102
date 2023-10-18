using System;


namespace R5T.S0102
{
    public class ProjectPathsOperator : IProjectPathsOperator
    {
        #region Infrastructure

        public static IProjectPathsOperator Instance { get; } = new ProjectPathsOperator();


        private ProjectPathsOperator()
        {
        }

        #endregion
    }
}
