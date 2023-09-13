using System;

using R5T.T0131;


namespace R5T.S0102.Platform
{
    /// <summary>
    /// Type suffixes for use in identity names.
    /// </summary>
    [ValuesMarker]
    public partial interface ITypeNameAffixes : IValuesMarker
    {
        /// <summary>
        /// <para>"[]" (open- and close-bracket pair)</para>
        /// </summary>
        public string ForArray_Suffix => "[]";

        /// <summary>
        /// <para>"@" (the at-sign, or alphasand)</para>
        /// Note: the suffix used by the C# type name for references is different ("&amp;", the ampersand).
        /// </summary>
        public string ForByReference_Suffix => "@";

        /// <summary>
        /// <para>"`" (tick)</para>
        /// </summary>
        public string ForGenericTypeParameterType_Prefix => "`";

        /// <summary>
        /// <para>"``" (double-tick)</para>
        /// </summary>
        public string ForGenericMethodParameterType_Prefix => "``";

        /// <summary>
        /// <para>"*" (the asterix)</para>
        /// </summary>
        public string ForPointer_Suffix => "*";
    }
}
