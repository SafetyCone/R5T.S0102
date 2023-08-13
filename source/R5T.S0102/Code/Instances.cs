using System;


namespace R5T.S0102
{
    public static class Instances
    {
        public static F0000.IArrayOperator ArrayOperator => F0000.ArrayOperator.Instance;
        public static T0212.F000.IDocumentationFileOperator DocumentationFileOperator => T0212.F000.DocumentationFileOperator.Instance;
        public static T0215.Z000.IDotnetPackNames DotnetPackNames => T0215.Z000.DotnetPackNames.Instance;
        public static T0215.Z000.IDotnetPackNameSets DotnetPackNameSets => T0215.Z000.DotnetPackNameSets.Instance;
        public static F0138.IDotnetPackPathOperator DotnetPackPathOperator => F0138.DotnetPackPathOperator.Instance;
        public static F0000.IFileOperator FileOperator => F0000.FileOperator.Instance;
        public static IFilePaths FilePaths => S0102.FilePaths.Instance;
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
        public static T0212.F000.IMemberElementOperator MemberElementOperator => T0212.F000.MemberElementOperator.Instance;
        public static Z0030.Raw.IMethodIdentityNames MethodIdentityNames_Raw => Z0030.Raw.MethodIdentityNames.Instance;
        public static F0000.INamespacedTypeNameOperator NamespacedTypeNameOperator => F0000.NamespacedTypeNameOperator.Instance;
        public static F0033.INotepadPlusPlusOperator NotepadPlusPlusOperator => F0033.NotepadPlusPlusOperator.Instance;
        public static F0000.INullOperator NullOperator => F0000.NullOperator.Instance;
        public static F0115.IOperations Operations_F0115 => F0115.Operations.Instance;
        public static Z0030.IPathologicalIdentityNames PathologicalIdentityNames => Z0030.PathologicalIdentityNames.Instance;
        public static Z0030.IPathologicalIdentityNameSets PathologicalIdentityNameSets => Z0030.PathologicalIdentityNameSets.Instance;
        public static Z0022.IRepositoriesDirectoryPaths RepositoriesDirectoryPaths => Z0022.RepositoriesDirectoryPaths.Instance;
        public static F0000.IStringOperator StringOperator => F0000.StringOperator.Instance;
        public static N002.Extensions.IStringOperator StringOperator_N002_Extensions => N002.Extensions.StringOperator.Instance;
        public static F0000.IStrings Strings => F0000.Strings.Instance;
        public static F0000.ISwitchOperator SwitchOperator => F0000.SwitchOperator.Instance;
        public static Z0057.ITargetFrameworkMonikers TargetFrameworkMonikers => Z0057.TargetFrameworkMonikers.Instance;
        public static F0124.ITextOperator TextOperator => F0124.TextOperator.Instance;
        public static T0159.F000.ITextOutputOperator TextOutputOperator => T0159.F000.TextOutputOperator.Instance;
        public static T0162.Z000.ITokenSeparators TokenSeparators => T0162.Z000.TokenSeparators.Instance;
        public static F0000.ITypeOperator TypeOperator => F0000.TypeOperator.Instance;
    }
}
