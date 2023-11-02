using System;
using System.Collections.Generic;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Request
{
    public class GetTokenRequest
    {
        public string CheckoutTokenizationId { get; set; }

        internal (bool, StringBuilder) Validate()
        {
            StringBuilder message = new StringBuilder();
            bool ret = true;

            try
            {
                if (String.IsNullOrEmpty(CheckoutTokenizationId))
                {
                    ret = false;
                    message.Append(" checkout-tokenization-id is empty.");
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
