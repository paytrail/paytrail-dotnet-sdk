namespace Paytrail_dotnet_sdk.Model.Response
{
    public class GetPaymentResponse: Response
    {
        public GetPaymentData Data { get; set; }
    }

    public class GetPaymentData
    {
        public string TransactionId { get; set; }
        public string Status { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string Reference { get; set; }
        public string Stamp { get; set; }
        public string CreatedAt { get; set; }
        public string Href { get; set; }
        public string Provider { get; set; }
        public string FilingCode { get; set; }
        public string PaidAt { get; set; }
    }
}
