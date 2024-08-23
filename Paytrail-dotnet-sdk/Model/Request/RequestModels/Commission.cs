using System;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Request.RequestModels
{
    public class Commission
    {
        public string Merchant { get; set; }
        public int Amount { get; set; }
        public (bool, StringBuilder) Validate()
        {
            bool ret = true;
            StringBuilder message = new StringBuilder();
            try
            {
                if (Merchant is null)
                {
                    message.Append(" commission's merchant is null or empty.");
                    ret = false;
                }

                //
                if (Amount < 0)
                {
                    message.Append(" commission's amount can't be a negative number.");
                    ret = false;
                }

                //
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
