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

        /// <summary>
        /// Identity names generated from members of an assembly, and found in the documentation file for that assembly.
        /// </summary>
        public HashSet<N002.IIdentityName> Found { get; } = new();

        /// <summary>
        /// Identity names generated from members of an assembly, but not found in the documentation file.
        /// </summary>
        public HashSet<N002.IIdentityName> NotFound { get; } = new();
        
        /// <summary>
        /// Identity names that are in the documentation file for an assembly, but not found among those generated from the members of the assembly.
        /// </summary>
        public HashSet<N002.IIdentityName> UnmatchedDocumentionFileIdentityNames { get; } = new();


        public AssemblyIdentityStringsGenerationResult(IAssemblyFilePath assemblyFilePath)
        {
            this.AssemblyFilePath = assemblyFilePath;
        }
    }
}
