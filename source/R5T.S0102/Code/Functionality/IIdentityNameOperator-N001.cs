using System;

using R5T.T0132;

using IKindMarkers = R5T.T0162.Z000.IKindMarkers;
using IInternalIdentityName = R5T.S0102.N002.IIdentityName;


namespace R5T.S0102.N001
{
    /// <summary>
    /// Works at the <see cref="IdentityName"/> level.
    /// </summary>
    [FunctionalityMarker]
    public partial interface IIdentityNameOperator : IFunctionalityMarker
    {
        private static T0162.F001.IIdentityNameOperator Base => T0162.F001.IdentityNameOperator.Instance;
        private static Internal.IIdentityNameOperator Internal => S0102.Internal.IdentityNameOperator.Instance;


        public IdentityName Get_StructuralIdentityName(IInternalIdentityName internalIdentityName)
        {
            var kindMarkerCharacter = Base.Get_KindMarker_Unchecked_CheckValidity(internalIdentityName.Value);

            Func<IInternalIdentityName, IdentityName> func = kindMarkerCharacter switch
            {
                IKindMarkers.Error_Constant => this.Get_Error,
                IKindMarkers.Event_Constant => this.Get_EventIdentityName,
                IKindMarkers.Field_Constant => this.Get_FieldIdentityName,
                IKindMarkers.Method_Constant => this.Get_MethodIdentityName,
                IKindMarkers.Namespace_Constant => this.Get_NamespaceIdentityName,
                IKindMarkers.Property_Constant => this.Get_PropertyIdentityName,
                IKindMarkers.Type_Constant => this.Get_TypeIdentityName,
                _ => throw Instances.SwitchOperator.GetUnrecognizedSwitchValueException(kindMarkerCharacter.ToString(), "Kind-Marker"),
            };

            var output = func(internalIdentityName);
            return output;
        }

        public IInternalIdentityName Get_InternalIdentityName(IdentityName identityName)
        {
            var kindMarkerCharacter = identityName.KindMarker.Value;

            Func<IdentityName, IInternalIdentityName> func = kindMarkerCharacter switch
            {
                IKindMarkers.Error_Constant => this.Get_Error,
                IKindMarkers.Event_Constant => this.Get_EventIdentityName,
                IKindMarkers.Field_Constant => this.Get_FieldIdentityName,
                IKindMarkers.Method_Constant => this.Get_MethodIdentityName,
                IKindMarkers.Namespace_Constant => this.Get_NamespaceIdentityName,
                IKindMarkers.Property_Constant => this.Get_PropertyIdentityName,
                IKindMarkers.Type_Constant => this.Get_TypeIdentityName,
                _ => throw Instances.SwitchOperator.GetUnrecognizedSwitchValueException(kindMarkerCharacter.ToString(), "Kind-Marker"),
            };

            var output = func(identityName);
            return output;
        }
    }
}
