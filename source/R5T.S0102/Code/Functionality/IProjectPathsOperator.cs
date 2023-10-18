using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using R5T.L0062.T000.Extensions;
using R5T.T0132;
using R5T.T0159;
using R5T.T0172;
using R5T.T0172.Extensions;


namespace R5T.S0102
{
    [FunctionalityMarker]
    public partial interface IProjectPathsOperator : IFunctionalityMarker
    {
        /// <summary>
        /// Foreach member, of all project assemblies, of all repositories, of all GitHub owners, perform the action.
        /// </summary>
        public async Task Foreach_OutputAssemblyMember(
            ITextOutput textOutput,
            Func<IProjectFilePath, IDocumentationXmlFilePath, IAssemblyFilePath, Assembly, MemberInfo, Task> action)
        {
            var projectFilePaths = Instances.FileSystemOperator.Find_AllProjectFilePaths(
                textOutput)
                .Select(projectFilePath => projectFilePath.ToProjectFilePath());

            var projectFilesTuples = Instances.ProjectPathOperator.CreateProjectFilesTuples(
                projectFilePaths,
                textOutput)
                .Where(tuple => Instances.FileSystemOperator.Exists_File(
                    tuple.AssemblyFilePath.Value)
                )
                .Now();

            foreach (var tuple in projectFilesTuples)
            {
                var assemblyFilePath = tuple.AssemblyFilePath;

                await Instances.ReflectionOperator.In_AssemblyContext(
                    assemblyFilePath.Value,
                    async assembly =>
                    {
                        // Foreach member element in the assembly (event, field, method, namespace, property, type), get the identity name.
                        await Instances.AssemblyOperator.Foreach_Member(
                            assembly,
                            async memberInfo =>
                            {
                                await action(
                                    tuple.ProjectFilePath,
                                    tuple.DocumentationFilePath,
                                    tuple.AssemblyFilePath,
                                    assembly,
                                    memberInfo);
                            });
                    });
            }
        }
    }
}
