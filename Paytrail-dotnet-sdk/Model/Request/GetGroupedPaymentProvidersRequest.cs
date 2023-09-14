using System;
using System.Linq;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Request
{
    public class GetGroupedPaymentProvidersRequest : GetPaymentProvidersRequest
    {
        public string Language { get; set; }

        internal new (bool, StringBuilder) Validate()
        {
            StringBuilder message = new StringBuilder();

            try
            {
                return base.Validate();
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

            if (String.IsNullOrEmpty(Language))
            {
                query += $"&language={Language}";
            }

            return query;
        }
    }
}
