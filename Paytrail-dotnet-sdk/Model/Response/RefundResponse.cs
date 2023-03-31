namespace Paytrail_dotnet_sdk.Model.Response
{
    public class RefundResponse: Response
    {
        public RefundData data { get; set; }
    }

    public class RefundData
    {
        public string transactionId { get; set; }
        public string provider { get; set; }
        public string status { get; set; }
    }
}
