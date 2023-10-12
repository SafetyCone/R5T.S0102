using System;
using System.Collections.Generic;

using R5T.T0172;
using R5T.L0062.T000;


namespace R5T.S0102.N001
{
    /// <summary>
    /// For use in matching generated identity names to documentation identity names for a given assembly file path.
    /// </summary>
    public class AssemblyIdentityStringsGenerationResult
    {
        public IAssemblyFilePath AssemblyFilePath { get; }
        public List<IIdentityString> Exceptions { get; } = new();
        public List<IdentityStringPair> Successes { get; } = new();
        public List<IdentityStringPair> Failures { get; } = new();


        public AssemblyIdentityStringsGenerationResult(IAssemblyFilePath assemblyFilePath)
        {
            this.AssemblyFilePath = assemblyFilePath;
        }
    }
}
