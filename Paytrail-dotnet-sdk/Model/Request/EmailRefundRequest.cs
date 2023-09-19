using Paytrail_dotnet_sdk.Model.Request.RequestModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Paytrail_dotnet_sdk.Model.Request
{
    public class EmailRefundRequest
    {
        public int? Amount { get; set; }
        public string Email { get; set; }
        public RefundItem[] Items { get; set; }
        public CallbackUrl CallbackUrls { get; set; }

        internal (bool, StringBuilder) Validate()
        {
            bool ret = true;
            StringBuilder message = new StringBuilder();
            try
            {
                if (Amount.HasValue && Amount < 0)
                {
                    ret = false;
                    message.Append(" item's unitPrice can't be a negative number.");
                }

                if (Items != null)
                {
                    foreach (var item in Items)
                    {
                        (bool isSuccess, StringBuilder valMess) = item.Validate();
                        if (!isSuccess)
                        {
                            ret = false;
                            message.Append(valMess);
                            break;
                        }
                    }
                }

                //
                if (CallbackUrls is null)
                {
                    ret = false;
                    message.Append(" object commission can't be null.");
                }
                else
                {
                    (bool isSuccess, StringBuilder valMess) = CallbackUrls.Validate();
                    if (!isSuccess)
                    {
                        message.Append(valMess);
                        return (false, valMess);
                    }
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
