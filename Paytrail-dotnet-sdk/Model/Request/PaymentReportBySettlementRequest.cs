using System;
using System.Collections.Generic;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Request
{
    public class PaymentReportBySettlementRequest
    {
        public string RequestType { get; set; }
        public string CallbackUrl { get; set; }
        public List<string> ReportFields { get; set; }
        public int? Submerchant { get; set; }

        internal (bool, StringBuilder) Validate()
        {
            StringBuilder message = new StringBuilder();
            bool ret = true;

            try
            {
                if (String.IsNullOrEmpty(RequestType))
                {
                    ret = false;
                    message.Append(" RequestType can't be null or empty.");
                }

                if (RequestType != "json" && RequestType != "csv")
                {
                    ret = false;
                    message.Append(" RequestType must be json or csv.");
                }

                if (String.IsNullOrEmpty(CallbackUrl))
                {
                    ret = false;
                    message.Append(" CallbackUrl can't be null or empty.");
                }

                return (ret, message);
            }
            catch (Exception ex)
            {
                message.Append(" " + ex.Message + ".");
                return (false, message);
            }
        }
    }
}
