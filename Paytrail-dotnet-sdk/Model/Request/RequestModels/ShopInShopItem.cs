using System;
using System.Collections.Generic;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Request.RequestModels
{
    public class ShopInShopItem : Item
    {
        public string orderId { get; set; }
        public string stamp { get; set; }
        public string reference { get; set; }
        public string merchant { get; set; }
        public Commission commission { get; set; }

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
                if (itemShopInShop.orderId is null)
                {
                    message.Append(" Item's orderId can't be null.");
                    ret = false;
                }

                //
                if (string.IsNullOrEmpty(itemShopInShop.stamp))
                {
                    message.Append(" Item's stamp can't be null or empty.");
                    ret = false;
                }

                //
                if (itemShopInShop.reference is null)
                {
                    message.Append(" Item's reference can't be null.");
                    ret = false;
                }

                //
                if (string.IsNullOrEmpty(itemShopInShop.merchant))
                {
                    message.Append(" Item's merchant can't be null or empty.");
                    ret = false;
                }

                //

                if (commission is null)
                {
                    ret = false;
                    message.Append(" object commission can't be null.");
                }
                else
                {
                    (isSuccess, valMess) = commission.Validate();

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
