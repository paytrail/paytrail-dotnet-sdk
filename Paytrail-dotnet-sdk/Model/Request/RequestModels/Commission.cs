using System;
using System.Collections.Generic;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Request.RequestModels
{
    public class Commission
    {
        public string merchant { get; set; }
        public int amount { get; set; }
        public (bool, StringBuilder) Validate()
        {
            bool ret = false;
            StringBuilder message = new StringBuilder();
            try
            {
                if (merchant is null)
                {
                    message.Append(" commission's merchant is null or empty.");
                    ret = false;
                }

                //
                if (amount < 0)
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
