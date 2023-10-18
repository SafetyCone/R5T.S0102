using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using R5T.L0062.T000.Extensions;
using R5T.T0132;
using R5T.T0162;
using R5T.T0172;
using R5T.T0172.Extensions;
using R5T.T0179.Extensions;
using R5T.T0215;
using R5T.T0218;

using R5T.S0102.N002.Extensions;


namespace R5T.S0102
{
    [FunctionalityMarker]
    public partial interface IScripts : IFunctionalityMarker
    {
        public void GenerateAndCheck_DotnetPackMemberIdentityStrings_FromSignatures()
        {
            /// Inputs.
            bool showSuccesses = false;

            var dotnetPackName = Instances.DotnetPackNames.Microsoft_NETCore_App_Ref;
            var targetFramework = Instances.TargetFrameworkMonikers.NET_6;
            var outputFilePath = Instances.FilePaths.OutputTextFilePath;

            
            /// Run.
            var (humanOutputTextFilePath, logFilePath) = Instances.TextOutputOperator.In_TextOutputContext(
                nameof(GenerateAndCheck_DotnetPackMemberIdentityStrings_FromSignatures),
                textOutput =>
                {
                    var results = new Dictionary<IAssemblyFilePath, N001.AssemblyIdentityStringsGenerationResult>();

                    Instances.DotnetPackPathOperator.Foreach_MemberInfo(
                        dotnetPackName,
                        targetFramework,
                        textOutput,
                        (memberInfo, assembly, assemblyFilePath, documentationFilePath) =>
                        {
                            //// For debugging - Select based on analysis of MemberInfo.
                            //if (assemblyFilePath.Value != @"C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.22\ref\net6.0\System.Threading.Channels.dll")
                            //{
                            //    return;
                            //}

                            // Generate the identity string.
                            // Note: this should always work, as it was tested in R5T.S0102.
                            var referenceIdentityString = Instances.MemberInfoOperator.Get_IdentityString(memberInfo);

                            Console.WriteLine(referenceIdentityString);

                            //System.Collections.Immutable.ImmutableArray.ToImmutableArray

                            //// For debugging.
                            //if (referenceIdentityString.Value != "M:System.Threading.Channels.Channel`2.op_Implicit(System.Threading.Channels.Channel{`0,`1})~System.Threading.Channels.ChannelReader{`1}")
                            //{
                            //    return;
                            //}

                            var result = Instances.DictionaryOperator.Acquire_Value(
                                results,
                                assemblyFilePath,
                                () => new N001.AssemblyIdentityStringsGenerationResult(assemblyFilePath));

                            // Try working with signatures.
                            try
                            {
                                var signature = Instances.SignatureOperator.Get_Signature(memberInfo);

                                var identityString = Instances.SignatureOperator.Get_IdentityString(signature);

                                var pair = new N001.IdentityStringPair
                                {
                                    FromStructure = identityString,
                                    Reference = referenceIdentityString
                                };

                                var areEqual = identityString.Equals(referenceIdentityString);
                                if(areEqual)
                                {
                                    result.Successes.Add(pair);
                                }
                                else
                                {
                                    result.Failures.Add(pair);
                                }
                            }
                            catch (Exception)
                            {
                                result.Exceptions.Add(referenceIdentityString);
                            }
                        });

                    Instances.Operator.Write_ResultsToOutput(
                        outputFilePath,
                        results,
                        showSuccesses);
                });

            Instances.NotepadPlusPlusOperator.Open(
                outputFilePath);
        }

        public async Task GenerateAndCheck_DotnetPackMemberIdentityStrings()
        {
            /// Inputs.
            bool showGeneratedButNotInDocumentation = false;

            var dotnetPackName = Instances.DotnetPackNames.Microsoft_NETCore_App_Ref;
            var targetFramework = Instances.TargetFrameworkMonikers.NET_6;
            var outputFilePath = Instances.FilePaths.OutputTextFilePath;

            /// Run.
            var (humanOutputTextFilePath, logFilePath) = await Instances.TextOutputOperator.In_TextOutputContext(
                nameof(GenerateAndCheck_DotnetPackMemberIdentityStrings),
                async textOutput =>
                {
                    // Foreach pair of assembly and documentation files, create all the member identity names contained in the assembly, match them against those in the hash.

                    var results = new Dictionary<IAssemblyFilePath, AssemblyIdentityStringsGenerationResult>();

                    await Instances.DotnetPackPathOperator.Foreach_DotnetPackAssemblyFile(
                        dotnetPackName,
                        targetFramework,
                        async (assemblyFilePath, documentationFilePath) =>
                        {
                            //// For debugging.
                            //if (assemblyFilePath.Value != @"C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.22\ref\net6.0\System.Runtime.dll")
                            //{
                            //    return;
                            //}

                            textOutput.WriteInformation($"{assemblyFilePath}\n\tProcessing...");

                            // Load the documentation file, and get all identity names in the documention file.
                            var documentationFileIdentityNames = await Instances.DocumentationFileOperator.Get_IdentityNames(documentationFilePath);

                            var documentationFileIdentityNamesHash = documentationFileIdentityNames.ToHashSet();

                            var result = new AssemblyIdentityStringsGenerationResult(assemblyFilePath);

                            // Load the assembly,
                            Instances.ReflectionOperator.In_AssemblyContext(
                                assemblyFilePath.Value,
                                assembly =>
                                {
                                    // Foreach member element in the assembly (event, field, method, namespace, property, type), get the identity name.
                                    Instances.AssemblyOperator.Foreach_Member(
                                        assembly,
                                        memberInfo =>
                                        {
                                            // Create the identity name from the member info.
                                            var identityName = Instances.MemberInfoOperator.Get_IdentityString(memberInfo)
                                                .Value.ToInternalIdentityName();

                                            //// For debugging.
                                            //if (identityName.Value != "M:System.IO.Enumeration.FileSystemEnumerable`1.#ctor(System.String,System.IO.Enumeration.FileSystemEnumerable}.FindTransform,System.IO.EnumerationOptions)")
                                            //{
                                            //    return;
                                            //}

                                            // Match each identity named member element to something in the documention file.
                                            var identityNameFound = documentationFileIdentityNamesHash.Contains(identityName);
                                            if (identityNameFound)
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
                        });

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

        /// <summary>
        /// For a big mess of identity strings, parse identity strings to a structured type instance, the serialize the instance.
        /// Compare the output to ensure all identity strings can be handled.
        /// </summary>
        /// <returns></returns>
        public async Task RoundTripParse_IdentityNames()
        {
            /// Inputs.
            var identityNames =
                //await Instances.IdentityNameSets.All
                //new IIdentityName[]
                //{
                //    //Instances.IdentityNames.Error_Basic,
                //    //Instances.IdentityNames.Namespace_Basic,
                //    //"T:StartupHook".ToIdentityName()
                //}
                (await Instances.IdentityNameSets.All)
                    .Where(
                        //Instances.IdentityNameOperator.Is_TypeIdentityName
                        //Instances.IdentityNameOperator.Is_EventIdentityName
                        //Instances.IdentityNameOperator.Is_FieldIdentityName
                        //Instances.IdentityNameOperator.Is_MethodIdentityName
                        Instances.IdentityNameOperator.Is_PropertyIdentityName
                    )
                ;
            var outputFilePath = Instances.FilePaths.OutputTextFilePath;
            var humanOutputFilePath = Instances.FilePaths.HumanOutputTextFilePath;
            var logFilePath = Instances.FilePaths.LogFilePath;


            /// Run.

            // Analysis
            // From here: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/#id-strings
            //  * There is no whitespace in the strings.
            //  * The start is a single character, followed by a colon. Example: "M:"
            //      * P:R5T.F0121.ICharacters.ErrorKindMarker
            //      * P:R5T.T0162.Z000.IMemberKindMarkers.ErrorKindMarker
            //  * There is no hash-sign (#) in any names.
            //  * If a name has a period in it (like the name of an explicitly implemented interface, or .ctor constructor methods), then periods are converted to hash-signs (#).
            //  * For properties and methods, the parameter list is enclosed in parentheses.
            //  * If no parameters are present, then there are no parentheses.
            //  * Parameters are separated by commas.
            //  * There are some complexities for pointers, byrefs, CMOD_OPT (?), and arrays.
            //  * Conversion operators include the return value, using ~ (tilde) as a separator.
            //  * Generic types use ` (back-tick) and then the number of type parameters.
            //  * Parameter types are then represented using a back-tick, then a zero-based index `0 for the generic type.
            //  * There are a variety of types that the C# compiler never generates (like pinned, required, function-pointers, ...).

            // Questions:
            //  1. What does the "out" modifier look like?
            //  2. What do default (optional) parameters look like?

            // External types to use:
            //  * Use the R5T.T0162.IIdentityName types, since they are just string wrappers.
            //  => We will create identity name structural types (IdentityName), and our our IHasX, IXed, and IX descriptive interface types (IIdentityName, which implements IHasKindMarker, etc.)

            // External types NOT to use:
            //  * Do NOT use the R5T.T0161.

            // Types needed:
            //  * Operator method names, string (e.g. op_Addition, ...).
            //  * Kind markers, character (e.g. E, F, P, ...).
            //  * Identity name structural types.
            //  * Identity name related IHasX, IXed, and IX descriptive interface types.

            // Structural types:
            //  * All types are kind-marked, but that's it! Namespace identity strings are not namespaced type named, and errors are just straight strings!

            await Instances.TextOutputOperator.In_TextOutputContext(
                humanOutputFilePath,
                nameof(Get_DotnetPackIdentityNames),
                logFilePath,
                async textOutput =>
                {
                    // This is the list of identity names that were not correctly round-trip deserialized.
                    var unmatchedIdentityNames = new List<(IIdentityName, IIdentityName)>();

                    foreach (var identityName in identityNames)
                    {
                        textOutput.WriteInformation("{identityName}: processing...", identityName);

                        // Process:
                        //  1. Convert the external (R5T.T0162) identity name strong-type to an internal (N002) identity name strong-type.
                        //  2. Based on the kind-marker value, decompose the internal (N002) identity name strong-type into constituent internal (N002) strongly-typed pieces.
                        //  3. Construct a new instance of the structural (N001) identity name type using the strongly-typed pieces.
                        //  4. Based on the type of the structural (N001) type, serialize components to strings.
                        //  5. Output an internal (N002) identity name strong-type.
                        //  6. Convert the internal (N002) identity name strong-type to an external (R5T.T0162) identity name strong-type.

                        try
                        {
                            var internalIdentityName = Instances.IdentityNameOperator_N002.ToInternalIdentityName(identityName);

                            var structuralIdentityName = Instances.IdentityNameOperator_N001.Get_StructuralIdentityName(internalIdentityName);

                            var outputInternalIdentityName = Instances.IdentityNameOperator_N001.Get_InternalIdentityName(structuralIdentityName);

                            var outputIdentityName = Instances.IdentityNameOperator_N002.ToExternalIdentityName(outputInternalIdentityName);

                            //// Compare, using the actual type, since it should match the derived external identity name type that went in!
                            //var equal = identityName.Equals(outputIdentityName);
                            // Compare, using the value, since input types might not be typed by their indicated kind-marker.
                            var equal = identityName.Equals_ByValue(outputIdentityName);
                            if (!equal)
                            {
                                unmatchedIdentityNames.Add((identityName, outputIdentityName));
                            }
                        }
                        catch
                        {
                            unmatchedIdentityNames.Add((identityName, "<error>".ToExternalIdentityName()));
                        }
                    }

                    if (unmatchedIdentityNames.Any())
                    {
                        var lines = unmatchedIdentityNames
                            .OrderAlphabetically(x => x.Item1.Value)
                            .Select(x => $"{x.Item1.Value}:\n\t{x.Item2.Value}")
                            ;

                        Instances.FileOperator.Write_Lines_Synchronous(
                            outputFilePath.Value,
                            lines);
                    }
                    else
                    {
                        Instances.FileOperator.Write_Lines_Synchronous(
                            outputFilePath.Value,
                            "No unmatched identity names.");
                    }
                });

            Instances.NotepadPlusPlusOperator.Open(
                humanOutputFilePath.Value,
                outputFilePath.Value);
        }

        /// <summary>
        /// For all Rivet (and personal) projects, accumulate all unique identity names found in documentation files,
        /// and then write these identity names to a central file.
        /// </summary>
        /// <returns></returns>
        public async Task Get_RivetIdentityNames()
        {
            /// Inputs.
            var outputFilePath = Instances.FilePaths.RivetIdentityNames;
            var humanOutputFilePath = Instances.FilePaths.HumanOutputTextFilePath;
            var logFilePath = Instances.FilePaths.LogFilePath;


            /// Run.
            var repositoriesDirectoryPaths = Instances.RepositoriesDirectoryPaths.AllOfMine;

            var identityNamesHash = new HashSet<IIdentityName>();

            await Instances.TextOutputOperator.In_TextOutputContext(
                humanOutputFilePath,
                nameof(Get_DotnetPackIdentityNames),
                logFilePath,
                async textOutput =>
                {
                    var projectFilePaths = Instances.FileSystemOperator.GetAllProjectFilePaths_FromRepositoriesDirectoryPaths(
                        repositoriesDirectoryPaths,
                        textOutput)
                        .Select(x => x.ToProjectFilePath())
                        .Now();

                    // Use the /publish directory path.
                    var documentationXmlFilePaths = projectFilePaths
                        .Select(projectFilePath =>
                        {
                            try
                            {
                                var output = Instances.Operations_F0115.CreateProjectFilesTuple(
                                    projectFilePath,
                                    textOutput)
                                    .DocumentationFilePath;

                                return output;
                            }
                            catch
                            {
                                return "".ToDocumentationXmlFilePath();
                            }
                        })
                        .Where(x => x.Value != "")
                        .Now();

                    foreach (var documentationXmlFilePath in documentationXmlFilePaths)
                    {
                        try
                        {
                            var memberElements = await Instances.DocumentationFileOperator.Get_MemberElements(
                                documentationXmlFilePath,
                                textOutput);

                            foreach (var memberElement in memberElements)
                            {
                                var identityName = Instances.MemberElementOperator.Get_IdentityName(memberElement);

                                identityNamesHash.Add(identityName);
                            }
                        }
                        catch
                        {
                            // Do nothing.
                        }
                    }
                });

            var lines = identityNamesHash
                .Select(x => x.Value)
                .OrderAlphabetically()
                ;

            Instances.FileOperator.Write_Lines_Synchronous(
                outputFilePath.Value,
                lines);

            Instances.NotepadPlusPlusOperator.Open(
                humanOutputFilePath.Value,
                outputFilePath.Value);
        }

        /// <summary>
        /// For all dotnet packs, accumulate all unique identity names in documentation files,
        /// and then write to a central file.
        /// </summary>
        public async Task Get_DotnetPackIdentityNames()
        {
            /// Inputs.
            // Output to a cloud drive file.
            var outputFilePath = Instances.FilePaths.DotnetPackIdentityNames;
            var humanOutputFilePath = Instances.FilePaths.HumanOutputTextFilePath;
            var logFilePath = Instances.FilePaths.LogFilePath;


            /// Run.
            var dotnetPackNames = Instances.DotnetPackNameSets.WithRefData;

            var targetFrameworkMonikersByDotnetPackNames = dotnetPackNames.ToDictionary(
                x => x,
                x => Get_TargetFrameworkMoniker(x));

            var identityNamesHash = new HashSet<IIdentityName>();

            await Instances.TextOutputOperator.In_TextOutputContext(
                humanOutputFilePath,
                nameof(Get_DotnetPackIdentityNames),
                logFilePath,
                async textOutput =>
                {
                    // Get all documentation file paths.
                    var documentationXmlFilePaths = new List<IDocumentationXmlFilePath>();

                    foreach (var pair in targetFrameworkMonikersByDotnetPackNames)
                    {
                        var dotnetPackName = pair.Key;
                        var targetFrameworkMoniker = pair.Value;

                        var packDocumentationXmlFilePaths = Instances.DotnetPackPathOperator.Get_DocumentationXmlFilePaths(
                            dotnetPackName,
                            targetFrameworkMoniker,
                            textOutput);

                        documentationXmlFilePaths.AddRange(packDocumentationXmlFilePaths);
                    }

                    foreach (var documentationXmlFilePath in documentationXmlFilePaths)
                    {
                        var memberElements = await Instances.DocumentationFileOperator.Get_MemberElements(
                            documentationXmlFilePath,
                            textOutput);

                        foreach (var memberElement in memberElements)
                        {
                            var identityName = Instances.MemberElementOperator.Get_IdentityName(memberElement);

                            identityNamesHash.Add(identityName);
                        }
                    }
                });

            var lines = identityNamesHash
                .Select(x => x.Value)
                .OrderAlphabetically()
                ;

            Instances.FileOperator.Write_Lines_Synchronous(
                outputFilePath.Value,
                lines);

            Instances.NotepadPlusPlusOperator.Open(
                humanOutputFilePath.Value,
                outputFilePath.Value);

            static ITargetFrameworkMoniker Get_TargetFrameworkMoniker(IDotnetPackName dotnetPackName)
            {
                var isNetStandard = dotnetPackName.Equals(Instances.DotnetPackNames.NETStandard_Library_Ref);

                var output = isNetStandard
                    ? Instances.TargetFrameworkMonikers.NET_Standard2_1
                    : Instances.TargetFrameworkMonikers.NET_6
                    ;

                return output;
            }
        }
    }
}
