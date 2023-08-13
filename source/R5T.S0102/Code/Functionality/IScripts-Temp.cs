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

            await Instances.TextOutputOperator.InTextOutputContext(
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
                            unmatchedIdentityNames.Add((identityName, "<error>".ToIdentityName()));
                        }
                    }

                    if (unmatchedIdentityNames.Any())
                    {
                        var lines = unmatchedIdentityNames
                            .OrderAlphabetically(x => x.Item1.Value)
                            .Select(x => $"{x.Item1.Value}:\n\t{x.Item2.Value}")
                            ;

                        Instances.FileOperator.WriteLines_Synchronous(
                            outputFilePath,
                            lines);
                    }
                    else
                    {
                        Instances.FileOperator.WriteText_Synchronous(
                            outputFilePath,
                            "No unmatched identity names.");
                    }
                });

            Instances.NotepadPlusPlusOperator.Open(
                humanOutputFilePath,
                outputFilePath);
        }
    }
}

