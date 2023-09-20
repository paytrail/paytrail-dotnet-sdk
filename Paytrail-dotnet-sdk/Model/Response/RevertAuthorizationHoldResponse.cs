namespace Paytrail_dotnet_sdk.Model.Response
{
    public class RevertAuthorizationHoldResponse : Response
    {
        public RevertAuthorizationHoldData Data { get; set; }
    }

    public class RevertAuthorizationHoldData
    {
        public string TransactionId { get; set; }
    }
}
