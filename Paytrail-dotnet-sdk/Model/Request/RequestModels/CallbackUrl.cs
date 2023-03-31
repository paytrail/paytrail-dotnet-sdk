using System;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Request.RequestModels
{
    public class CallbackUrl
    {
        public string success { get; set; }
        public string cancel { get; set; }

        public (bool, StringBuilder) Validate()
        {
            bool ret = true;
            StringBuilder message = new StringBuilder();

            try
            {
                if (success is null)
                {
                    ret = false;
                    message.Append(" url success can't be null.");
                }

                //
                if (cancel is null)
                {
                    ret = false;
                    message.Append(" url cancel can't be null.");
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
