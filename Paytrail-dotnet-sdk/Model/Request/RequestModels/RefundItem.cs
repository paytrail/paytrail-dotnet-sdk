using System;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Request.RequestModels
{
    public class RefundItem
    {
        public int Amount { get; set; }
        public string Stamp { get; set; }
        public string RefundStamp { get; set; }
        public string RefundReference { get; set; }
        public Commission Commission { get; set; }

        internal (bool, StringBuilder) Validate()
        {
            bool ret = true;
            StringBuilder message = new StringBuilder();
            try
            {
                if(Amount < 0)
                {
                    ret = false;
                    message.Append(" item's unitPrice can't be a negative number.");
                }

                //
                if (string.IsNullOrEmpty(Stamp))
                {
                    ret = false;
                    message.Append(" item's stamp can't be null or empty.");
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

                //
                if(Commission is null)
                {
                    ret = false;
                    message.Append(" object commission can't be null.");
                } 
                else
                {
                    (bool isSuccess, StringBuilder valMess) = Commission.Validate();
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
