using System;
using System.Collections.Generic;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Response
{
    public class CreateMitPaymentResponse : Response
    {
        public CreateMitPaymentChargeData Data { get; set; }
    }

    public class CreateMitPaymentChargeData
    {
        public string TransactionId { get; set; }
        public string ThreeDSecureUrl { get; set; }
    }
}
