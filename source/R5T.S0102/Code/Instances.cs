using System;


namespace R5T.S0102
{
    public class Instances :
        L0055.Instances
    {
        public static F0000.IArrayOperator ArrayOperator => F0000.ArrayOperator.Instance;
        public static L0058.IAssemblyFilePathOperator AssemblyFilePathOperator => L0058.AssemblyFilePathOperator.Instance;
        public static F0018.IAssemblyOperator AssemblyOperator => F0018.AssemblyOperator.Instance;
        public static IDocumentationFileOperator DocumentationFileOperator => S0102.DocumentationFileOperator.Instance;
        public static T0215.Z000.IDotnetPackNames DotnetPackNames => T0215.Z000.DotnetPackNames.Instance;
        public static T0215.Z000.IDotnetPackNameSets DotnetPackNameSets => T0215.Z000.DotnetPackNameSets.Instance;
        public static L0053.IEnumerableOperator EnumerableOperator => L0053.EnumerableOperator.Instance;
        public static F0000.IFileOperator FileOperator => F0000.FileOperator.Instance;
        public static new IFilePaths FilePaths => S0102.FilePaths.Instance;
        public static F0082.IFileSystemOperator FileSystemOperator => F0082.FileSystemOperator.Instance;
        public static IIdentityNameOperator IdentityNameOperator => S0102.IdentityNameOperator.Instance;
        public static Internal.IIdentityNameOperator IdentityNameOperator_Internal => Internal.IdentityNameOperator.Instance;
        public static N001.IIdentityNameOperator IdentityNameOperator_N001 => N001.IdentityNameOperator.Instance;
        public static N002.IIdentityNameOperator IdentityNameOperator_N002 => N002.IdentityNameOperator.Instance;
        public static Z0030.IIdentityNames IdentityNames => Z0030.IdentityNames.Instance;
        public static Z0030.Z001.IIdentityNameSets IdentityNameSets => Z0030.Z001.IdentityNameSets.Instance;
        public static F0000.IIntegerOperator IntegerOperator => F0000.IntegerOperator.Instance;
        public static T0162.Z000.IKindMarkers KindMarkers => T0162.Z000.KindMarkers.Instance;
        public static T0162.Z000.IKindMarkerSets KindMarkerSets => T0162.Z000.KindMarkerSets.Instance;
        public static L0053.IMethodInfoOperator MethodInfoOperator => L0053.MethodInfoOperator.Instance;
        public static T0212.F000.IMemberElementOperator MemberElementOperator => T0212.F000.MemberElementOperator.Instance;
        public static IMemberInfoOperator MemberInfoOperator => S0102.MemberInfoOperator.Instance;
        public static Z0030.Raw.IMethodIdentityNames MethodIdentityNames_Raw => Z0030.Raw.MethodIdentityNames.Instance;
        public static F0000.INamespacedTypeNameOperator NamespacedTypeNameOperator => F0000.NamespacedTypeNameOperator.Instance;
        public static F0000.INullOperator NullOperator => F0000.NullOperator.Instance;
        public static F0115.IOperations Operations_F0115 => F0115.Operations.Instance;
        public static IParameterInfoOperator ParameterInfoOperator => S0102.ParameterInfoOperator.Instance;
        public static Z0030.IPathologicalIdentityNames PathologicalIdentityNames => Z0030.PathologicalIdentityNames.Instance;
        public static Z0030.IPathologicalIdentityNameSets PathologicalIdentityNameSets => Z0030.PathologicalIdentityNameSets.Instance;
        public static L0053.IPropertyInfoOperator PropertyInfoOperator => L0053.PropertyInfoOperator.Instance;
        public static L0057.IReflectionOperator ReflectionOperator => L0057.ReflectionOperator.Instance;
        public static Z0022.IRepositoriesDirectoryPaths RepositoriesDirectoryPaths => Z0022.RepositoriesDirectoryPaths.Instance;
        public static F0000.IStringOperator StringOperator => F0000.StringOperator.Instance;
        public static N002.Extensions.IStringOperator StringOperator_N002_Extensions => N002.Extensions.StringOperator.Instance;
        public static F0000.ISwitchOperator SwitchOperator => F0000.SwitchOperator.Instance;
        public static Z0057.ITargetFrameworkMonikers TargetFrameworkMonikers => Z0057.TargetFrameworkMonikers.Instance;
        public static F0124.ITextOperator TextOperator => F0124.TextOperator.Instance;
        public static T0162.Z000.ITokenSeparators TokenSeparators => T0162.Z000.TokenSeparators.Instance;
        public static Platform.ITypeNameAffixes TypeNameAffixes => Platform.TypeNameAffixes.Instance;
        public static ITypeOperator TypeOperator => S0102.TypeOperator.Instance;
    }
}
