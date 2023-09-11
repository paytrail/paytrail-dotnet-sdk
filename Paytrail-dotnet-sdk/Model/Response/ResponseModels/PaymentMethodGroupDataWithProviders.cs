using Paytrail_dotnet_sdk.Model.Request.RequestModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Response.ResponseModels
{
    public class PaymentMethodGroupDataWithProviders
    {
        public PaymentMethodGroup Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Svg { get; set; }
        public List<Provider> Providers { get; set; }

    }
}
