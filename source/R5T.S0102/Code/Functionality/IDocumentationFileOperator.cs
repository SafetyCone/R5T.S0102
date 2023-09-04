using System;
using System.Linq;
using System.Threading.Tasks;

using R5T.T0132;
using R5T.T0172;

using R5T.S0102.N002;
using R5T.S0102.N002.Extensions;


namespace R5T.S0102
{
    [FunctionalityMarker]
    public partial interface IDocumentationFileOperator : IFunctionalityMarker,
        T0212.F000.IDocumentationFileOperator
    {
        private static T0212.F000.Platform.IDocumentationFileOperator DocumentationFileOperator_Platform => T0212.F000.Platform.DocumentationFileOperator.Instance;


        public async Task<IIdentityName[]> Get_IdentityNames(IDocumentationXmlFilePath documentationXmlFilePath)
        {
            var strings = await DocumentationFileOperator_Platform.Get_IdentityNames(documentationXmlFilePath);

            var output = strings
                .Select(x => x.ToIdentityName())
                .Now();

            return output;
        }
    }
}
