using System;
using Paytrail_dotnet_sdk.Model.Request.RequestModels;
using System.Text;

namespace Paytrail_dotnet_sdk.Model.Request
{
    public class PayAddCardRequest : Request
    {
        public string Stamp { get; set; }
        public string Reference { get; set; }
        public int Amount { get; set; }
        public string Currency { get; set; }
        public string Language { get; set; }
        public string OrderId { get; set; }
        public Item[] Items { get; set; }
        public Customer Customer { get; set; }
        public Address DeliveryAddress { get; set; }
        public Address InvoicingAddress { get; set; }
        public bool ManualInvoiceActivation { get; set; }
        public CallbackUrl RedirectUrls { get; set; }
        public CallbackUrl CallbackUrls { get; set; }
        public int CallbackDelay { get; set; }
        public string[] Groups { get; set; }
        public bool UsePricesWithoutVat { get; set; }

        internal (bool, StringBuilder) Validate()
        {
            StringBuilder message = new StringBuilder();
            try
            {
                bool ret = true;
                if (string.IsNullOrEmpty(Stamp))
                {
                    ret = false;
                    message.Append(" stamp can't be null or empty.");
                }
                else
                {
                    if (Stamp.Length > 200)
                    {
                        ret = false;
                        message.Append(" stamp is more than 200 characters.");
                    }
                }

                //
                if (Reference is null)
                {
                    ret = false;
                    message.Append(" reference can't be null.");
                }
                else
                {
                    if (Reference.Length > 200)
                    {
                        ret = false;
                        message.Append(" reference is more than 200 characters.");
                    }
                }

                //
                if (OrderId is null)
                {
                    ret = false;
                    message.Append(" orderId can't be null.");
                }

                //
                if (Amount < 0)
                {
                    ret = false;
                    message.Append(" amount can't be less than zero.");
                }
                else
                {
                    if (Amount > 99999999)
                    {
                        ret = false;
                        message.Append(" amount can't be more than 99999999.");
                    }
                }

                //
                if (Customer is null)
                {
                    ret = false;
                    message.Append(" object customer can't be null.");
                }
                else
                {
                    (bool isSuccess, StringBuilder valMess) = Customer.Validate();
                    if (!isSuccess)
                    {
                        ret = false;
                        message.Append(valMess);
                    }
                }

                //
                if (Items is null)
                {
                    ret = false;
                    message.Append(" object items can't be null.");
                }
                else
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
                if (DeliveryAddress is null)
                {
                    ret = false;
                    message.Append(" object deliveryAddress can't be null.");
                }
                else
                {
                    (bool isSuccess, StringBuilder valMess) = DeliveryAddress.Validate();

                    if (!isSuccess)
                    {
                        ret = false;
                        message.Append(valMess);
                    }
                }

                //
                if (InvoicingAddress is null)
                {
                    ret = false;
                    message.Append(" object invoicingAddress can't be null.");
                }
                else
                {
                    (bool isSuccess, StringBuilder valMess) = InvoicingAddress.Validate();

                    if (!isSuccess)
                    {
                        ret = false;
                        message.Append(valMess);
                    }
                }

                //
                if (RedirectUrls is null)
                {
                    ret = false;
                    message.Append(" object redirectUrls can't be null.");
                }
                else
                {
                    (bool isSuccess, StringBuilder valMess) = RedirectUrls.Validate();
                    if (!isSuccess)
                    {
                        ret = false;
                        message.Append(valMess);
                    }
                }

                //
                if (CallbackUrls is null)
                {
                    ret = false;
                    message.Append(" object callbackUrls can't be null.");
                }
                else
                {
                    (bool isSuccess, StringBuilder valMess) = CallbackUrls.Validate();
                    if (!isSuccess)
                    {
                        ret = false;
                        message.Append(valMess);
                    }
                }

                //
                if (Groups is null)
                {
                    ret = false;
                    message.Append(" object groups can't be null.");
                }
                else
                {
                    for (int i = 0; i < Groups.Length; i++)
                    {
                        bool flagContain = false;

                        foreach (var item in Enum.GetValues(typeof(PaymentMethodGroup)))
                        {
                            if (Groups[i] == item.ToString())
                            {
                                flagContain = true;
                            }
                        }

                        //
                        if (!flagContain)
                        {
                            ret = false;
                            message.Append(" value " + Groups[i] + " is not in list payment method");
                        }
                    }
                }

                //
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
