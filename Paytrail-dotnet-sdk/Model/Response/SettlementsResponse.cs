using System;
using System.Collections.Generic;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Response
{
    public class SettlementsResponse : Response
    {
        public List<SettlementsData> Data { get; set; }
    }

    public class SettlementsData
    {
        public int Id { get; set; }
        public string SettlementReference { get; set; }
        public string CreatedAt { get; set; }
        public string SettledAt { get; set; }
    }
}
