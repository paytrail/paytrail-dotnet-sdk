using Paytrail_dotnet_sdk.Model.Request.RequestModels;

namespace Paytrail_dotnet_sdk.Model.Response.ResponseModels
{
    public class Provider
    {
        public string Url { get; set; }
        public string Icon { get; set; }
        public string Svg { get; set; }
        public string Name { get; set; }
        public PaymentMethodGroup Group { get; set; }
        public string Id { get; set; }
        public FormField[] Parameters { get; set; }
    }
}
