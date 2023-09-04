using System;


namespace R5T.S0102
{
    public class DocumentationFileOperator : IDocumentationFileOperator
    {
        #region Infrastructure

        public static IDocumentationFileOperator Instance { get; } = new DocumentationFileOperator();


        private DocumentationFileOperator()
        {
        }

        #endregion
    }
}
