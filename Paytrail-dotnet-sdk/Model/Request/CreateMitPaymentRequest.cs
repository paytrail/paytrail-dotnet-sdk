using System;
using System.Collections.Generic;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Request
{
    public class CreateMitPaymentRequest : PaymentRequest
    {
        public string Token { get; set; }

        internal new (bool, StringBuilder) Validate()
        {
            StringBuilder message = new StringBuilder();
            try
            {
                (bool isValid, StringBuilder valMess) = base.Validate();

                bool ret = isValid;
                message.Append(valMess.ToString());

                if (String.IsNullOrEmpty(Token))
                {
                    ret = false;
                    message.Append(" token can't be null.");
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
