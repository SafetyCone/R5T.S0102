using System;


namespace R5T.S0102
{
    public class FilePaths : IFilePaths
    {
        #region Infrastructure

        public static IFilePaths Instance { get; } = new FilePaths();


        private FilePaths()
        {
        }

        #endregion
    }
}
