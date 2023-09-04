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
        public async Task GenerateAndCheck_DotnetPackMemberIdentityNames()
        {
            /// Inputs.
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

                    // Foreach pair,
                    foreach (var pair in filePathPairs.PairedFilePaths)
                    {
                        // Load the documentation file, and get all identity names in the documention file.
                        var documentationFileIdentityNames = await Instances.DocumentationFileOperator.Get_IdentityNames(pair.Value);

                        // Load the assembly,
                        Instances.ReflectionOperator.InAssemblyContext(
                            pair.Key.Value,
                            assembly =>
                            {
                                // Foreach member element in the assembly (event, field, method, namespace, property, type), get the identity name.
                                Instances.AssemblyOperator.Get_TypesInAssembly
                                // Match each identity named member element to something in the documention file.
                                // If no match is found, add the assembly file path and member identity name to a list for the assembly.
                                // If any documentation identity names are not removed, add them to the output for the assembly.
                            });
                    }
                });

            Instances.NotepadPlusPlusOperator.Open(humanOutputTextFilePath);
        }
    }
}

