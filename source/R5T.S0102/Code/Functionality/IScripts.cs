using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using R5T.T0132;
using R5T.T0162;
using R5T.T0172;
using R5T.T0172.Extensions;
using R5T.T0215;
using R5T.T0218;


namespace R5T.S0102
{
    [FunctionalityMarker]
    public partial interface IScripts : IFunctionalityMarker
    {
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

            await Instances.TextOutputOperator.InTextOutputContext(
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

            Instances.FileOperator.WriteLines_Synchronous(
                outputFilePath.Value,
                lines);

            Instances.NotepadPlusPlusOperator.Open(
                humanOutputFilePath,
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

            await Instances.TextOutputOperator.InTextOutputContext(
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

            Instances.FileOperator.WriteLines_Synchronous(
                outputFilePath.Value,
                lines);

            Instances.NotepadPlusPlusOperator.Open(
                humanOutputFilePath,
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
