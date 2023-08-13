using System;

using R5T.T0132;
using R5T.T0162;
using R5T.T0162.Extensions;

using R5T.S0102.N002.Extensions;

using IExternalIdentityName = R5T.T0162.IIdentityName;
using StructuralIdentityName = R5T.S0102.N001.IdentityName;


namespace R5T.S0102.N002
{
    /// <summary>
    /// Works at the <see cref="IIdentityName"/> level.
    /// </summary>
    [FunctionalityMarker]
    public partial interface IIdentityNameOperator : IFunctionalityMarker
    {
        private static T0162.F001.IIdentityNameOperator Base => T0162.F001.IdentityNameOperator.Instance;


        public IIdentityName Get_IdentityName(
            IKindMarker kindMarker,
            IIdentityNameValue value)
        {
            var output = Base.Get_IdentityName(
                kindMarker.Value,
                value.Value)
                .ToInternalIdentityName();

            return output;
        }
         
        public IIdentityNameValue Get_Value(
            IIdentityName identityName)
        {
            var output = Base.Get_IdentityNameValue_Unchecked(identityName.Value)
                .ToIdentityNameValue();

            return output;
        }

        public IKindMarker Get_KindMarker(IIdentityName identityName)
        {
            var output = Base.Get_KindMarker_Unchecked_CheckValidity(identityName.Value)
                .ToKindMarker();

            return output;
        }

        public bool Is_KindMarker(
            IIdentityName identityName,
            IKindMarker kindMarker)
        {
            var output = Base.Is_KindMarker_Unchecked(
                identityName.Value,
                kindMarker.Value);

            return output;
        }

        /// <summary>
        /// Returns a <see cref="IExternalIdentityName"/> instance that is actually of <see cref="IExternalIdentityName"/> type.
        /// (Same base type, regardless of kind-marker implied type.)
        /// </summary>
        public IExternalIdentityName ToExternalIdentityName_OfBaseType(IIdentityName identityName)
        {
            var output = T0162.Extensions.StringOperator.Instance.ToIdentityName(identityName.Value);
            return output;
        }

        /// <summary>
        /// Returns a <see cref="IExternalIdentityName"/> instance whose type is actually one of descendant external identity name types.
        /// (The type matches kind-marker implied type.)
        /// </summary>
        public IExternalIdentityName ToExternalIdentityName_OfKindMarkedType(IIdentityName identityName)
        {
            var output = Base.ToIdentityName_OfKindMarkedType(identityName.Value);
            return output;
        }

        /// <summary>
        /// Chooses the <see cref="ToExternalIdentityName_OfKindMarkedType(IIdentityName)"/> as the default.
        /// </summary>
        public IExternalIdentityName ToExternalIdentityName(IIdentityName identityName)
        {
            var output = this.ToExternalIdentityName_OfKindMarkedType(identityName);
            return output;
        }

        public IIdentityName ToInternalIdentityName(IExternalIdentityName externalIdentityName)
        {
            var output = externalIdentityName.Value.ToInternalIdentityName();
            return output;
        }

        public StructuralIdentityName ToStructuralIdentityName(IIdentityName identityName)
        {
            var output = Instances.IdentityNameOperator_N001.Get_StructuralIdentityName(identityName);
            return output;
        }

        public void Verify_IsKindMarker(
            IIdentityName identityName,
            IKindMarker kindMarker)
        {
            Base.Verify_IsKindMarker(
                identityName.Value,
                kindMarker.Value);
        }
    }
}
