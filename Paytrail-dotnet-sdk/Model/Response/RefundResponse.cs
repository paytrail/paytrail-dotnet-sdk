namespace Paytrail_dotnet_sdk.Model.Response
{
    public class RefundResponse: Response
    {
        public RefundData Data { get; set; }
    }

    public class RefundData
    {
        public string TransactionId { get; set; }
        public string Provider { get; set; }
        public string Status { get; set; }
    }
}
