using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using R5T.T0132;
using R5T.T0162;
using R5T.T0162.Extensions;
using R5T.T0172;
using R5T.T0172.Extensions;
using R5T.T0179.Extensions;
using R5T.T0215;
using R5T.T0218;


namespace R5T.S0102
{
    public partial interface IScripts
    {
        /// <summary>
        /// For use in matching generated identity names to documentation identity names for a given assembly file path.
        /// </summary>
        public class AssemblyIdentityNamesGenerationResult
        {
            public IAssemblyFilePath AssemblyFilePath { get; }
            public HashSet<N002.IIdentityName> Found { get; } = new();
            public HashSet<N002.IIdentityName> NotFound { get; } = new();
            public HashSet<N002.IIdentityName> UnmatchedDocumentionFileIdentityNames { get; } = new();


            public AssemblyIdentityNamesGenerationResult(IAssemblyFilePath assemblyFilePath)
            {
                this.AssemblyFilePath = assemblyFilePath;
            }
        }

        public async Task GenerateAndCheck_DotnetPackMemberIdentityNames()
        {
            /// Inputs.
            bool showGeneratedButNotInDocumentation = false;

            var dotnetPackName = Instances.DotnetPackNames.Microsoft_NETCore_App_Ref;
            var targetFramework = Instances.TargetFrameworkMonikers.NET_6;
            var outputFilePath = Instances.FilePaths.OutputTextFilePath;

            /// Run.
            var (humanOutputTextFilePath, logFilePath) = await Instances.TextOutputOperator.In_TextOutputContext(
                nameof(GenerateAndCheck_DotnetPackMemberIdentityNames),
                async textOutput =>
                {
                    // Get the dotnet pack directory path.
                    var dotnetDirectoryPath = Instances.DotnetPackPathOperator.Get_DotnetPackDirectoryPath(
                        dotnetPackName,
                        targetFramework);

                    // Get the pairs of assembly file-documentation file outputs in the dotnet pack directory.
                    var filePathPairs = Instances.AssemblyFilePathOperator.Get_PairedAssemblyXmlDocumentationFilePaths(
                        dotnetDirectoryPath);

                    // Foreach pair of assembly and documentation files, create all the member identity names contained in the assembly, match them against those in the hash.

                    var results = new Dictionary<IAssemblyFilePath, AssemblyIdentityNamesGenerationResult>();

                    foreach (
                        var pair in filePathPairs.PairedFilePaths
                            //// For debugging.
                            //.Where(pair => pair.Key.Value == @"C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.22\ref\net6.0\System.Runtime.dll")
                        )
                    {
                        textOutput.WriteInformation($"{pair.Key}\n\tProcessing...");

                        // Load the documentation file, and get all identity names in the documention file.
                        var documentationFileIdentityNames = await Instances.DocumentationFileOperator.Get_IdentityNames(pair.Value);

                        var documentationFileIdentityNamesHash = documentationFileIdentityNames.ToHashSet();

                        var result = new AssemblyIdentityNamesGenerationResult(pair.Key);

                        // Load the assembly,
                        Instances.ReflectionOperator.In_AssemblyContext(
                            pair.Key.Value,
                            assembly =>
                            {
                                // Foreach member element in the assembly (event, field, method, namespace, property, type), get the identity name.
                                Instances.AssemblyOperator.Foreach_Member(
                                    assembly,
                                    memberInfo =>
                                    {
                                        // Create the identity name from the member info.
                                        var identityName = Instances.MemberInfoOperator.Get_IdentityName(memberInfo);

                                        // Match each identity named member element to something in the documention file.
                                        var identityNameFound = documentationFileIdentityNamesHash.Contains(identityName);
                                        if(identityNameFound)
                                        {
                                            // If a match is found, remove the identity name from the documentation file's hash.
                                            documentationFileIdentityNamesHash.Remove(identityName);

                                            // Then add the identity name to the found hash.
                                            result.Found.Add(identityName);
                                        }
                                        else
                                        {
                                            // If no match is found, add the identity name to the not found hash.
                                            result.NotFound.Add(identityName);
                                        }
                                    });

                                // If any documentation identity names are not removed, add them to the output for the assembly.
                                result.UnmatchedDocumentionFileIdentityNames.AddRange(documentationFileIdentityNamesHash);
                            });

                        results.Add(result.AssemblyFilePath, result);
                    }

                    // Output results.
                    var resultsToOutput = results
                        // Drop the dictionary now.
                        .Select(pair => pair.Value)
                        // Only show results from assemblies where there are identity names in the documentation that were not generated.
                        .Where(result => result.UnmatchedDocumentionFileIdentityNames.Any() || showGeneratedButNotInDocumentation)
                        .Now();

                    var anyResultsToOutput = resultsToOutput.Any();

                    var lines = Instances.EnumerableOperator.From("Matched and unmatched identity names between .NET pack assemblies and their documentation files.")
                        .AppendIf(anyResultsToOutput, resultsToOutput
                            .SelectMany(result =>
                            {
                                var output = Instances.EnumerableOperator.From($"{result.AssemblyFilePath}:")
                                    .Append(Instances.EnumerableOperator.From("Unmatched (in documentation, but not generated):")
                                        .AppendIf(result.UnmatchedDocumentionFileIdentityNames.Any(), result.UnmatchedDocumentionFileIdentityNames
                                            .Select(x => $"\t{x}")
                                        )
                                        .AppendIf(!result.UnmatchedDocumentionFileIdentityNames.Any(),
                                            Instances.EnumerableOperator.From("<None>")
                                        )
                                    )
                                    .Append(Instances.EnumerableOperator.From("Not found (generated, but not in documentation):")
                                        .AppendIf(result.NotFound.Any(), result.NotFound
                                            .Select(x => $"\t{x}")
                                        )
                                        .AppendIf(!result.NotFound.Any(),
                                            Instances.EnumerableOperator.From("<None>")
                                        )
                                    )
                                    .Append(Instances.EnumerableOperator.From("Found:")
                                        .AppendIf(result.Found.Any(), result.Found
                                            .Select(x => $"\t{x}")
                                        )
                                        .AppendIf(!result.Found.Any(),
                                            Instances.EnumerableOperator.From("<None>")
                                        )
                                    )
                                    ;

                                return output;
                            })
                        )
                        .AppendIf(!anyResultsToOutput,
                            Instances.EnumerableOperator.From("< None >\nNo unmatched identity names between .NET pack assemblies and their documentation files.")
                        );

                    Instances.FileOperator.Write_Lines_Synchronous(
                        outputFilePath.Value,
                        lines);
                });

            Instances.NotepadPlusPlusOperator.Open(
                outputFilePath);
        }
    }
}

