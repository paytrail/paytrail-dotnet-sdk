using Paytrail_dotnet_sdk.Model.Response.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Response
{
    public class GetGroupedPaymentProvidersResponse : Response
    {
        public GetGroupedPaymentProvidersData Data { get; set; }
    }

    public class GetGroupedPaymentProvidersData
    {
        public string Terms { get; set; }
        public List<PaymentMethodGroupDataWithProviders> Groups { get; set; }
        public List<Provider> Providers { get; set; }
    }
}
