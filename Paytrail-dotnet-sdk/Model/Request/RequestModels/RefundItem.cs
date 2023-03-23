using System;
using System.Collections.Generic;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Request.RequestModels
{
    public class RefundItem
    {
        public int amount { get; set; }
        public string stamp { get; set; }
        public string refundStamp { get; set; }
        public string refundReference { get; set; }
        public Commission commission { get; set; }

        internal (bool, StringBuilder) Validate()
        {
            bool ret = true;
            StringBuilder message = new StringBuilder();
            try
            {
                if(amount < 0)
                {
                    ret = false;
                    message.Append(" item's unitPrice can't be a negative number.");
                }

                //
                if (string.IsNullOrEmpty(stamp))
                {
                    ret = false;
                    message.Append(" item's stamp can't be null or empty.");
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

                //
                if(commission is null)
                {
                    ret = false;
                    message.Append(" object commission can't be null.");
                } 
                else
                {
                    (bool isSuccess, StringBuilder valMess) = commission.Validate();
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
