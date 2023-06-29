﻿using Paytrail_dotnet_sdk.Model.Request.RequestModels;
using System;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Request
{
    public class RefundRequest
    {
        public int Amount { get; set; }
        public string Email { get; set; }
        public string RefundStamp { get; set; }
        public string RefundReference { get; set; }
        public RefundItem[] Items { get; set; }
        public CallbackUrl CallbackUrls { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; //Coordinated Universal Time (UTC)

        internal (bool, StringBuilder) Validate()
        {
            bool ret = true;
            StringBuilder message = new StringBuilder();
            try
            {
                if (Amount < 0)
                {
                    ret = false;
                    message.Append(" item's unitPrice can't be a negative number.");
                }

                //
                if (RefundStamp is null)
                {
                    ret = false;
                    message.Append(" item's refundStamp can't be null.");
                }

                //
                if (RefundReference is null)
                {
                    ret = false;
                    message.Append(" item's refundReference can't be null.");
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
