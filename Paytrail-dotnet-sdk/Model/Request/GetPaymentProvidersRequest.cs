using Paytrail_dotnet_sdk.Model.Request.RequestModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Request
{
    public class GetPaymentProvidersRequest : Request
    {
        public int Amount { get; set; }
        public List<PaymentMethodGroup> Groups { get; set; }

        internal (bool, StringBuilder) Validate()
        {
            StringBuilder message = new StringBuilder();
            try
            {
                bool ret = true;
                if (Amount <= 0)
                {
                    ret = false;
                    message.Append(" amount cannot be less than or equal to 0.");
                }

                return (ret, message);
            }
            catch (Exception ex)
            {
                message.Append(" " + ex.Message + ".");
                return (false, message);
            }
        }

        public override string ToString()
        {
            string query = "";

            if (Amount > 0)
            {
                query += $"&amount={Amount}";
            }

            if (Groups != null && Groups.Count > 0)
            {
                string groupsString = string.Join(",", Groups.Select(group => group.ToString().ToLower()));

                query += $"&groups={groupsString}";
            }

            return query;
        }
    }
}
