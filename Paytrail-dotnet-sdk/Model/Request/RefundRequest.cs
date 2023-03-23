using Paytrail_dotnet_sdk.Model.Request.RequestModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Request
{
    public class RefundRequest
    {
        public int amount { get; set; }
        public string email { get; set; }
        public string refundStamp { get; set; }
        public string refundReference { get; set; }
        public RefundItem[] items { get; set; }
        public CallbackUrl callbackUrls { get; set; }

        internal (bool, StringBuilder) Validate()
        {
            bool ret = true;
            StringBuilder message = new StringBuilder();
            try
            {
                if (amount < 0)
                {
                    ret = false;
                    message.Append(" item's unitPrice can't be a negative number.");
                }

                //
                if (refundStamp is null)
                {
                    ret = false;
                    message.Append(" item's refundStamp can't be null.");
                }

                //
                if (refundReference is null)
                {
                    ret = false;
                    message.Append(" item's refundReference can't be null.");
                }

                if (items != null)
                {
                    foreach (var item in items)
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
                if (callbackUrls is null)
                {
                    ret = false;
                    message.Append(" object commission can't be null.");
                }
                else
                {
                    (bool isSuccess, StringBuilder valMess) = callbackUrls.Validate();
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
