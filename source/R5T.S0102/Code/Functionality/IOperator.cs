using System;
using System.Collections.Generic;
using System.Linq;

using R5T.T0132;
using R5T.T0172;
using R5T.T0181;


namespace R5T.S0102
{
    [FunctionalityMarker]
    public partial interface IOperator : IFunctionalityMarker
    {
        public void Write_ResultsToOutput(
            ITextFilePath outputFilePath,
            IDictionary<IAssemblyFilePath, N001.AssemblyIdentityStringsGenerationResult> results,
            bool showSuccesses = true)
        {
            // Now write out results.
            var resultsToOutput = results
                .Where(pair => pair.Value.Exceptions.Any() || pair.Value.Failures.Any() || (pair.Value.Successes.Any() && showSuccesses))
                .Select(pair => pair.Value)
                ;

            var lines = Instances.EnumerableOperator.From("Identity string pairs (from-signature and from-member as reference) in .NET pack assemblies.")
                .AppendIf(resultsToOutput.Any(), resultsToOutput
                    .SelectMany(result =>
                    {
                        var output = Instances.EnumerableOperator.From($"{result.AssemblyFilePath}:")
                            .AppendIf(result.Exceptions.Any(), Instances.EnumerableOperator.From($"{result.Exceptions.Count} - Exceptions (identity strings):")
                                .Append(result.Exceptions
                                    .Select(x => $"\t{x.Value}")
                                )
                            )
                            .AppendIf(!result.Exceptions.Any(), "<No exceptions>")
                            .AppendIf(result.Failures.Any(), Instances.EnumerableOperator.From($"{result.Failures.Count} - Failures (from signature/reference identity string):")
                                .Append(result.Failures
                                    .Select(x => $"\t{x.FromStructure}\n\t{x.Reference}\n")
                                )
                            )
                            .AppendIf(!result.Failures.Any(), "<No failures>")
                            .AppendIf(result.Successes.Any() && showSuccesses, Instances.EnumerableOperator.From($"{result.Successes.Count} - Successes (signature/identity strings):")
                                .Append(result.Successes
                                    .Select(x => $"\t{x.FromStructure}\n\t{x.Reference}")
                                )
                            )
                            .AppendIf(!result.Successes.Any() && showSuccesses, "<No successes>")
                            ;

                        return output;
                    })
                )
                .AppendIf(!resultsToOutput.Any(), "=> All identity string pairs matched.")
                ;

            Instances.FileOperator.Write_Lines_Synchronous(
                outputFilePath.Value,
                lines);
        }
    }
}
