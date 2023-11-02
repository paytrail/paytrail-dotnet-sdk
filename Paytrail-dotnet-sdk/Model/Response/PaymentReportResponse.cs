namespace Paytrail_dotnet_sdk.Model.Response
{
    public class PaymentReportResponse : Response
    {
        public PaymentReportData Data { get; set; }
    }

    public class PaymentReportData
    {
        public string RequestId { get; set; }
    }
}
