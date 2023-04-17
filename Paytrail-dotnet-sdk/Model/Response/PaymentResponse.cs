using Paytrail_dotnet_sdk.Model.Response.ResponseModels;

namespace Paytrail_dotnet_sdk.Model.Response
{
    public class PaymentResponse : Response
    {
        public PaymentData Data { get; set; }
    }

    public class PaymentData
    {
        public string TransactionId { get; set; }
        public string Href { get; set; }
        public string Reference { get; set; }
        public string Terms { get; set; }
        public PaymentMethodGroupData[] Groups { get; set; }
        public Provider[] Providers { get; set; }
        public ApplePay CustomProviders { get; set; }
    }
}






