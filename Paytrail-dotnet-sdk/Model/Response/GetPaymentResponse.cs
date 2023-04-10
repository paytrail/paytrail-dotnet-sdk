namespace Paytrail_dotnet_sdk.Model.Response
{
    public class GetPaymentResponse: Response
    {
        public GetPaymentData data { get; set; }
    }

    public class GetPaymentData
    {
        public string transactionId { get; set; }
        public string status { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
        public string reference { get; set; }
        public string stamp { get; set; }
        public string createdAt { get; set; }
        public string href { get; set; }
        public string provider { get; set; }
        public string filingCode { get; set; }
        public string paidAt { get; set; }
    }
}
