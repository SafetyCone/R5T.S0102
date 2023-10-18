using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using R5T.L0062.T000;
using R5T.L0062.T000.Extensions;
using R5T.T0132;
using R5T.T0162;
using R5T.T0162.Extensions;
using R5T.T0172;
using R5T.T0172.Extensions;
using R5T.T0179.Extensions;
using R5T.T0215;
using R5T.T0218;

using R5T.S0102.N002.Extensions;


namespace R5T.S0102
{
    public partial interface IScripts
    {
        /// <summary>
        /// Foreach member, of all project assemblies, of all repositories, of all GitHub owners,
        /// generate identity strings and validate them against the identity strings found in the output documentation file.
        /// </summary>
        public async Task GenerateAndCheck_CodebaseIdentityStrings_FromSignatures()
        {
            /// Inputs.
            bool showGeneratedButNotInDocumentation = false;

            var outputFilePath = Instances.FilePaths.OutputTextFilePath;


            /// Run.
            var (humanOutputTextFilePath, logFilePath) = await Instances.TextOutputOperator.In_TextOutputContext(
                nameof(GenerateAndCheck_CodebaseIdentityStrings_FromSignatures),
                async textOutput =>
                {
                    var results = new Dictionary<IProjectFilePath, ProjectIdentityStringsGenerationResult>();

                    var documentationFileIdentityStringHashes = new Dictionary<IProjectFilePath, HashSet<IIdentityString>>();

                    await Instances.ProjectPathsOperator.Foreach_OutputAssemblyMember(
                        textOutput,
                        async (projectFilePath, documentationFilePath, assemblyFilePath, assembly, memberInfo) =>
                        {
                            var result = Instances.DictionaryOperator.Acquire_Value(
                                results,
                                projectFilePath,
                                () => new ProjectIdentityStringsGenerationResult(projectFilePath));

                            var documentationFileIdentityStringsHash = await Instances.DictionaryOperator.Acquire_Value(
                                documentationFileIdentityStringHashes,
                                projectFilePath,
                                async () =>
                                {
                                    if(!Instances.FileSystemOperator.Exists_File(documentationFilePath.Value))
                                    {
                                        return new HashSet<IIdentityString>();
                                    }

                                    // Else, if the file does exist.
                                    var documentationFileIdentityNames = await Instances.DocumentationFileOperator.Get_IdentityNames(
                                        documentationFilePath);

                                    var documentationFileIdentityStrings = documentationFileIdentityNames
                                        .Select(x => x.Value.ToIdentityString())
                                        .Now();

                                    var documentationFileIdentityStringsHash = documentationFileIdentityStrings.ToHashSet();
                                    return documentationFileIdentityStringsHash;
                                });


                            var identityString = Instances.IdentityStringOperator.Get_IdentityString(memberInfo);

                            Console.WriteLine(identityString);

                            // Match each identity named member element to something in the documention file.
                            var identityNameFound = documentationFileIdentityStringsHash.Contains(identityString);
                            if (identityNameFound)
                            {
                                // If a match is found, remove the identity name from the documentation file's hash.
                                documentationFileIdentityStringsHash.Remove(identityString);

                                // Then add the identity name to the found hash.
                                result.Found.Add(identityString);
                            }
                            else
                            {
                                // If no match is found, add the identity name to the not found hash.
                                result.NotFound.Add(identityString);
                            }
                        });

                    // If any documentation file identity strings are not removed, add them to the output for the assembly.
                    foreach (var pair in documentationFileIdentityStringHashes)
                    {
                        if(pair.Value.Any())
                        {
                            var result = results[pair.Key];

                            result.UnmatchedDocumentionFileIdentityNames.AddRange(pair.Value);
                        }
                    }

                    Instances.Operator.Write_ResultsToOutput(
                        outputFilePath,
                        results,
                        showGeneratedButNotInDocumentation);
                });

            Instances.NotepadPlusPlusOperator.Open(
                outputFilePath);
        }
    }
}

