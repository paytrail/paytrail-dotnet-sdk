using Paytrail_dotnet_sdk.Model.Response.ResponseModels;

namespace Paytrail_dotnet_sdk.Model.Response
{
    public class PaymentResponse : Response
    {
        public PaymentData data { get; set; }
    }

    public class PaymentData
    {
        public string transactionId { get; set; }
        public string href { get; set; }
        public string reference { get; set; }
        public string terms { get; set; }
        public PaymentMethodGroupData[] groups { get; set; }
        public Provider[] providers { get; set; }
        public ApplePay customProviders { get; set; }
    }
}






