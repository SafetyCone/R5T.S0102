using System;
using System.Collections.Generic;

using R5T.T0172;
using R5T.L0062.T000;


namespace R5T.S0102
{
    /// <summary>
    /// For use in matching generated identity strings to documentation identity strings for the output assembly of a given project file path.
    /// </summary>
    public class ProjectIdentityStringsGenerationResult
    {
        public IProjectFilePath ProjectFilePath { get; }

        /// <summary>
        /// Identity strings generated from members of an assembly, and found in the documentation file for that assembly.
        /// </summary>
        public HashSet<IIdentityString> Found { get; } = new();

        /// <summary>
        /// Identity strings generated from members of an assembly, but not found in the documentation file.
        /// </summary>
        public HashSet<IIdentityString> NotFound { get; } = new();

        /// <summary>
        /// Identity strings that are in the documentation file for an assembly, but not found among those generated from the members of the assembly.
        /// </summary>
        public HashSet<IIdentityString> UnmatchedDocumentionFileIdentityNames { get; } = new();


        public ProjectIdentityStringsGenerationResult(IProjectFilePath projectFilePath)
        {
            this.ProjectFilePath = projectFilePath;
        }
    }
}
