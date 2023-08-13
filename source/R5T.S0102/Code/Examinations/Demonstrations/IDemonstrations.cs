using System;

using R5T.T0141;
using R5T.T0179.Extensions;


namespace R5T.S0102
{
    [DemonstrationsMarker]
    public partial interface IDemonstrations : IDemonstrationsMarker
    {
        public void RountTripParse_IdentityName()
        {
            /// Inputs.
            var identityName = Instances.MethodIdentityNames_Raw.N_005;


            /// Run.
            var internalIdentityName = Instances.IdentityNameOperator_N002.ToInternalIdentityName(identityName);

            var structuralIdentityName = Instances.IdentityNameOperator_N001.Get_StructuralIdentityName(internalIdentityName);

            var outputInternalIdentityName = Instances.IdentityNameOperator_N001.Get_InternalIdentityName(structuralIdentityName);

            var outputIdentityName = Instances.IdentityNameOperator_N002.ToExternalIdentityName(outputInternalIdentityName);

            //// Compare, using the actual type, since it should match the derived external identity name type that went in!
            //var equal = identityName.Equals(outputIdentityName);
            // Compare, using the value, since input types might not be typed by their indicated kind-marker.
            var equal = identityName.Equals_ByValue(outputIdentityName);
            if (!equal)
            {
                Console.WriteLine($"Unequal:\n{identityName}\n{outputIdentityName}");
            }
            else
            {
                Console.WriteLine($"Equal:\n{identityName}");
            }
        }

        public void Get_Arguments()
        {
            /// Inputs.
            var methodIdentityName = Instances.MethodIdentityNames_Raw.N_004;

            /// Run.
            var argumentList = Instances.IdentityNameOperator.Get_ArgumentList(methodIdentityName);

            var arguments = Instances.IdentityNameOperator.Get_Arguments(argumentList);

            Console.WriteLine(argumentList);

            foreach (var argument in arguments)
            {
                Console.WriteLine(argument);
            }
        }

        public void Get_ArgumentsList()
        {
            /// Inputs.
            var methodIdentityName = Instances.MethodIdentityNames_Raw.N_004;


            /// Run.
            var argumentList = Instances.IdentityNameOperator.Get_ArgumentList(methodIdentityName);

            Console.WriteLine($"{argumentList} for:\n\t{methodIdentityName}");
        }

        /// <summary>
        /// Demonstrates the use of <see cref="IIdentityNameOperator.Is_MinimallyValidIdentityName(T0162.IIdentityName)"/>.
        /// </summary>
        public void Is_MinimallyValidIdentityName()
        {
            /// Inputs.
            var validIdentityName = Instances.IdentityNames.Basic;
            var invalidIdentityNames = Instances.PathologicalIdentityNameSets.All;

            var isValidIdentityNameValid = Instances.IdentityNameOperator.Is_MinimallyValidIdentityName(validIdentityName);
            
            Console.WriteLine($"{validIdentityName}: is valid?\n\t{isValidIdentityNameValid}");

            foreach (var invalidIdentityName in invalidIdentityNames)
            {
                // Pretty-prints null and empty strings.
                var invalidIdentityNameTextRepresentation = Instances.TextOperator.Get_TextRepresentation(invalidIdentityName.Value);

                try
                {
                    var isInvalidIdentityNameInvalid = Instances.IdentityNameOperator.Is_MinimallyValidIdentityName(invalidIdentityName);

                    Console.WriteLine($"{invalidIdentityNameTextRepresentation}: is valid?\n\t{isInvalidIdentityNameInvalid}");
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"{invalidIdentityNameTextRepresentation}: Identity name validity exception.");
                    Console.WriteLine(exception);
                }
            }
        }
    }
}
