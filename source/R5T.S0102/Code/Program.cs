using System;
using System.Threading.Tasks;


namespace R5T.S0102
{
    class Program
    {
        //void Test()
        //{
        //    IWithKindMarker withKindMarker = null;

        //    withKindMarker.KindMarker = new KindMarker('a');

        //    Console.WriteLine(withKindMarker.KindMarker);
        //}

        static async Task Main()
        {
            //await Scripts.Instance.Get_DotnetPackIdentityNames();
            //await Scripts.Instance.Get_RivetIdentityNames();
            //await Scripts.Instance.RoundTripParse_IdentityNames();
            //await Scripts.Instance.GenerateAndCheck_DotnetPackMemberIdentityStrings();
            Scripts.Instance.GenerateAndCheck_DotnetPackMemberIdentityStrings_FromSignatures();
            //await Scripts.Instance.GenerateAndCheck_CodebaseIdentityStrings_FromSignatures();

            //Demonstrations.Instance.Is_MinimallyValidIdentityName();
            //Demonstrations.Instance.Get_ArgumentsList();
            //Demonstrations.Instance.Get_Arguments();
            //Demonstrations.Instance.RountTripParse_IdentityName();
        }
    }
}