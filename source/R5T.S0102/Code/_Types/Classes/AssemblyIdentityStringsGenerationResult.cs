using System;
using System.Collections.Generic;

using R5T.T0172;


namespace R5T.S0102
{
    /// <summary>
    /// For use in matching generated identity names to documentation identity names for a given assembly file path.
    /// </summary>
    public class AssemblyIdentityStringsGenerationResult
    {
        public IAssemblyFilePath AssemblyFilePath { get; }
        public HashSet<N002.IIdentityName> Found { get; } = new();
        public HashSet<N002.IIdentityName> NotFound { get; } = new();
        public HashSet<N002.IIdentityName> UnmatchedDocumentionFileIdentityNames { get; } = new();


        public AssemblyIdentityStringsGenerationResult(IAssemblyFilePath assemblyFilePath)
        {
            this.AssemblyFilePath = assemblyFilePath;
        }
    }
}
