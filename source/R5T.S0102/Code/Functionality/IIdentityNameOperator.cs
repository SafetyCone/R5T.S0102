using System;

using R5T.T0132;
using R5T.T0162;
using R5T.T0162.Extensions;

using R5T.S0102.N002;
using R5T.S0102.N002.Extensions;

using IIdentityName = R5T.T0162.IIdentityName;
using System.Linq;
using R5T.F0000;

namespace R5T.S0102
{
    /// <summary>
    /// Works at the <see cref="IIdentityName"/> level.
    /// </summary>
    [FunctionalityMarker]
    public partial interface IIdentityNameOperator : IFunctionalityMarker
    {
        private static T0162.F001.IIdentityNameOperator Base => T0162.F001.IdentityNameOperator.Instance;
        private static Internal.IIdentityNameOperator Internal => S0102.Internal.IdentityNameOperator.Instance;


        public IArgumentList Get_ArgumentList(IMethodIdentityName methodIdentityName)
        {
            var output = Internal.Get_ArgumentListValue(methodIdentityName.Value)
                .ToArgumentList();

            return output;
        }

        public IArgument[] Get_Arguments(IArgumentList argumentList)
        {
            var output = Internal.Get_Arguments(argumentList.Value)
                .Select(x => x.ToArgument())
                .ToArray();

            return output;
        }

        public IKindMarker Get_KindMarker(IIdentityName identityName)
        {
            var output = Base.Get_KindMarker_Unchecked_CheckValidity(identityName.Value)
                .ToKindMarker();

            return output;
        }

        public WasFound<ITypeName> Has_OutputType(IMethodIdentityName methodIdentityName)
        {
            var output = Internal.Has_OutputType(methodIdentityName.Value)
                .Convert(x => x.ToTypeName());

            return output;
        }

        public bool Is_MinimallyValidIdentityName(IIdentityName identityName)
        {
            var output = Base.Is_MinimallyValidIdentityName(identityName.Value);
            return output;
        }

        public bool Is_EventIdentityName(IIdentityName identityName)
        {
            var output = Internal.Is_EventIdentityName(identityName.Value);
            return output;
        }

        public bool Is_FieldIdentityName(IIdentityName identityName)
        {
            var output = Internal.Is_FieldIdentityName(identityName.Value);
            return output;
        }

        public bool Is_MethodIdentityName(IIdentityName identityName)
        {
            var output = Internal.Is_MethodIdentityName(identityName.Value);
            return output;
        }

        public bool Is_PropertyIdentityName(IIdentityName identityName)
        {
            var output = Internal.Is_PropertyIdentityName(identityName.Value);
            return output;
        }

        public bool Is_TypeIdentityName(IIdentityName identityName)
        {
            var output = Internal.Is_TypeIdentityName(identityName.Value);
            return output;
        }
    }
}
