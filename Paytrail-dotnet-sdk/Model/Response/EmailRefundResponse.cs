using System;
using System.Collections.Generic;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Response
{
    public class EmailRefundResponse : Response
    {
        public EmailRefundData Data { get; set; }
    }

    public class EmailRefundData : RefundData
    {
    }
}
