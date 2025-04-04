using System;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Request.RequestModels
{
    public class ShopInShopItem : Item
    {
        public string OrderId { get; set; }
        public string Merchant { get; set; }
        public Commission Commission { get; set; }

        public override (bool, StringBuilder) Validate()
        {
            bool ret = true;
            StringBuilder message = new StringBuilder();

            //
            try
            {
                (bool isSuccess, StringBuilder valMess) = base.Validate();
                if (UnitPrice < 0)
                {
                    ret = false;
                    valMess.Append(" item's unitPrice can't be a negative number.");
                }
                //
                if (!isSuccess)
                    return (false, valMess);


                //Unique identifier for this item. Required for Shop-in-Shop payments. Required for item refunds.
                if (string.IsNullOrEmpty(this.Stamp))
                {
                    message.Append(" Item's stamp can't be null or empty.");
                    ret = false;
                }

                //Reference for this item. Required for Shop-in-Shop payments.
                if (this.Reference is null)
                {
                    message.Append(" Item's reference can't be null.");
                    ret = false;
                }

                //Merchant ID for the item. Required for Shop-in-Shop payments, do not use for normal payments.
                if (string.IsNullOrEmpty(this.Merchant))
                {
                    message.Append(" Item's merchant can't be null or empty.");
                    ret = false;
                }

                //Shop-in-Shop commission. Do not use for normal payments.
                if (Commission is null)
                {
                    //ret = false;
                    //message.Append(" object commission can't be null.");
                }
                else
                {
                    (isSuccess, valMess) = Commission.Validate();

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
