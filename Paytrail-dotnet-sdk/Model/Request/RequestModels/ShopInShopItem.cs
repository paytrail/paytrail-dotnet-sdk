using System;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Request.RequestModels
{
    public class ShopInShopItem : Item
    {
        public string OrderId { get; set; }
        public string Stamp { get; set; }
        public string Reference { get; set; }
        public string Merchant { get; set; }
        public Commission Commission { get; set; }

        public (bool, StringBuilder) Validate(ShopInShopItem itemShopInShop)
        {
            bool ret = true;
            StringBuilder message = new StringBuilder();

            //
            try
            {
                Item item = itemShopInShop as Item;
                (bool isSuccess, StringBuilder valMess) = item.Validate();

                //
                if (!isSuccess)
                    return (false, valMess);

                //
                if (itemShopInShop.OrderId is null)
                {
                    message.Append(" Item's orderId can't be null.");
                    ret = false;
                }

                //
                if (string.IsNullOrEmpty(itemShopInShop.Stamp))
                {
                    message.Append(" Item's stamp can't be null or empty.");
                    ret = false;
                }

                //
                if (itemShopInShop.Reference is null)
                {
                    message.Append(" Item's reference can't be null.");
                    ret = false;
                }

                //
                if (string.IsNullOrEmpty(itemShopInShop.Merchant))
                {
                    message.Append(" Item's merchant can't be null or empty.");
                    ret = false;
                }

                //

                if (Commission is null)
                {
                    ret = false;
                    message.Append(" object commission can't be null.");
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
