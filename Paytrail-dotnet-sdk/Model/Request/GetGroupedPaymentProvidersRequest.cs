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
            string query = base.ToString();

            if (!String.IsNullOrEmpty(Language))
            {
                query += $"&language={Language}";
            }

            return query;
        }
    }
}
