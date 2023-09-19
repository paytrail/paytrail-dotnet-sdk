using System;
using System.Collections.Generic;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Response
{
    public class GetPaymentProvidersResponse : Response
    {
        public List<GetPaymentProvidersData> Data { get; set; }
    }

    public class GetPaymentProvidersData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Svg { get; set; }
        public string Group { get; set; }
    }
}
