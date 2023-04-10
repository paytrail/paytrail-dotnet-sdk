using Newtonsoft.Json;
using Paytrail_dotnet_sdk.Model.Request;
using Paytrail_dotnet_sdk.Model.Request.RequestModels;
using Paytrail_dotnet_sdk.Model.Response;

namespace Paytrail_dotnet_sdk.UnitTest
{
    public class PayTrailClient_CreatePaymentTests
    {
        const string MERCHANTIDN = "375917";
        const string SECRETKEYN = "SAIPPUAKAUPPIAS";
        const string MERCHANTIDSIS = "695861";
        const string SECRETKEYSIS = "MONISAIPPUAKAUPPIAS";
        const string MERCHANTIDSISS = "695874";

        #region Create Payment

        [Fact]
        public void CreateShopInShopPayment_RequestNull_ReturnCode400()
        {
            //Arrange
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.RequestNull;

            //Act
            PayTrailClient payTrail = new PayTrailClient(MERCHANTIDSIS, SECRETKEYSIS, "test");
            PaymentRequest? request = null;
            PaymentResponse res = payTrail.CreatePayment(request);
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateShopInShopPayment_ValidateFalse_ReturnCode403()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.ValidateFail;

            //Act
            PayTrailClient payTrail = new PayTrailClient(MERCHANTIDSIS, SECRETKEYSIS, "test");
            PaymentRequest request = new PaymentRequest();
            PaymentResponse res = payTrail.CreatePayment(request);
            int actual = res.ReturnCode;
            string a = res.ReturnMessage;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateShopInShopPayment_Success_ReturnCode200()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.Success;

            //Act
            PayTrailClient payTrail = new PayTrailClient(MERCHANTIDSIS, SECRETKEYSIS, "test");
            ShopInShopPaymentRequest request = new ShopInShopPaymentRequest()
            {
                stamp = Guid.NewGuid().ToString(),
                reference = "9187445",
                amount = 1590,
                currency = "EUR",
                language = "FI",
                orderId = "",
                items = new List<ShopInShopItem>()
            {
                new ShopInShopItem
                {
                    unitPrice = 1590,
                    units = 1,
                    vatPercentage = 24,
                    productCode = "#927502759",
                    category = "Pet supplies",
                    description = "Cat ladder",
                    merchant = MERCHANTIDSISS,
                    stamp = Guid.NewGuid().ToString(),
                    reference = "",
                    orderId = "",
                    commission = new Commission()
                    {
                        merchant = MERCHANTIDSISS,
                        amount = 159
                    }
                }
            }.ToArray(),
                customer = new Customer()
                {
                    email = "erja.esimerkki@example.org",
                    firstName = "Erja",
                    vatId = "FI12345671",
                    companyName = "nothing",
                    lastName = "+358501234567",
                    phone = "123",
                },
                redirectUrls = new CallbackUrl()
                {
                    success = "https://ecom.example.org/success",
                    cancel = "https://ecom.example.org/cancel",
                },

                callbackUrls = new CallbackUrl()
                {
                    success = "https://ecom.example.org/success",
                    cancel = "https://ecom.example.org/cancel"
                },
                deliveryAddress = new Address()
                {
                    city = "Tampere",
                    country = "FI",
                    county = "Pirkanmaa",
                    postalCode = "33100",
                    streetAddress = "Hämeenkatu 6 B"
                },
                invoicingAddress = new Address()
                {
                    city = "Helsinki",
                    country = "FI",
                    county = "Uusimaa",
                    postalCode = "00510",
                    streetAddress = "Testikatu 1"

                },
                groups = new List<string>()
            {
                PaymentMethodGroup.mobile.ToString()
            }.ToArray()};
            PaymentResponse res = payTrail.CreateShopInShopPayment(request);
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateShopInShopPayment_CallPayTrailReturnNull_ReturnCode404()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.ResponseNull;

            //Act
            PayTrailClient payTrail = new PayTrailClient(MERCHANTIDSIS, SECRETKEYSIS, "test");
            ShopInShopPaymentRequest request = new ShopInShopPaymentRequest()
            {
                stamp = Guid.NewGuid().ToString(),
                reference = "9187445",
                amount = 1590,
                currency = "EUR",
                language = "FI",
                orderId = "",
                items = new List<ShopInShopItem>()
            {
                new ShopInShopItem
                {
                    orderId = "",
                    unitPrice = 1590,
                    units = 1,
                    vatPercentage = 24,
                    productCode = "#927502759",
                    category = "Pet supplies",
                    description = "Cat ladder",
                    merchant = MERCHANTIDSISS,
                    stamp = Guid.NewGuid().ToString(),
                    reference = "",
                    commission = new Commission()
                    {
                        merchant = MERCHANTIDSISS,
                        amount = 159
                    }
                }
            }.ToArray(),
                customer = new Customer()
                {
                    email = "erja.esimerkki@example.org",
                    firstName = "Erja",
                    vatId = "FI12345671",
                    companyName = "nothing",
                    lastName = "+358501234567",
                    phone = "123",
                },
                redirectUrls = new CallbackUrl()
                {
                    success = "https://ecom.example.org/success",
                    cancel = "https://ecom.example.org/cancel",
                },

                callbackUrls = new CallbackUrl()
                {
                    success = "https://ecom.example.org/success",
                    cancel = "https://ecom.example.org/cancel"
                },
                deliveryAddress = new Address()
                {
                    city = "Tampere",
                    country = "FI",
                    county = "Pirkanmaa",
                    postalCode = "33100",
                    streetAddress = "Hämeenkatu 6 B"
                },
                invoicingAddress = new Address()
                {
                    city = "Helsinki",
                    country = "FI",
                    county = "Uusimaa",
                    postalCode = "00510",
                    streetAddress = "Testikatu 1"

                },
                groups = new List<string>()
            {
                PaymentMethodGroup.mobile.ToString()
            }.ToArray()

            };
            PaymentResponse res = payTrail.CreateShopInShopPayment(request);
            int actual = res.ReturnCode;
            string a = res.ReturnMessage;
            string json = JsonConvert.SerializeObject(request);
            //Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void CreateShopInShopPayment_CallPayTrailReturnFail_ReturnCode500()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.ResponseError;

            //Act
            PayTrailClient payTrail = new PayTrailClient(MERCHANTIDSIS, SECRETKEYSIS, "test");
            ShopInShopPaymentRequest request = new ShopInShopPaymentRequest()
            {
                stamp = Guid.NewGuid().ToString(),
                reference = "9187445",
                amount = 1590,
                currency = "EUR",
                language = "FI",
                orderId = "",
                items = new List<ShopInShopItem>()
            {
                new ShopInShopItem
                {
                    unitPrice = 1590,
                    units = 1,
                    vatPercentage = 24,
                    productCode = "#927502759",
                    category = "Pet supplies",
                    description = "Cat ladder",
                    merchant = MERCHANTIDSISS,
                    stamp = Guid.NewGuid().ToString(),
                    reference = ""
                }
            }.ToArray(),
                customer = new Customer()
                {
                    email = "erja.esimerkki@example.org",
                    firstName = "Erja",
                    vatId = "FI12345671",
                    companyName = "nothing",
                    lastName = "+358501234567",
                    phone = "123",
                },
                redirectUrls = new CallbackUrl()
                {
                    success = "https://ecom.example.org/success",
                    cancel = "https://ecom.example.org/cancel",
                },

                callbackUrls = new CallbackUrl()
                {
                    success = "https://ecom.example.org/success",
                    cancel = "https://ecom.example.org/cancel"
                },
                deliveryAddress = new Address()
                {
                    city = "Tampere",
                    country = "FI",
                    county = "Pirkanmaa",
                    postalCode = "33100",
                    streetAddress = "Hämeenkatu 6 B"
                },
                invoicingAddress = new Address()
                {
                    city = "Helsinki",
                    country = "FI",
                    county = "Uusimaa",
                    postalCode = "00510",
                    streetAddress = "Testikatu 1"

                },
                groups = new List<string>()
            {
                PaymentMethodGroup.mobile.ToString()
            }.ToArray()

            };
            PaymentResponse res = payTrail.CreateShopInShopPayment(request);
            int actual = res.ReturnCode;
            string a = res.ReturnMessage;

            //Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void CreateShopInShopPayment_CallPayException_ReturnCode503()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.Exception;

            //Act
            PayTrailClient payTrail = new PayTrailClient(MERCHANTIDSIS, SECRETKEYSIS, "test");
            ShopInShopPaymentRequest request = new ShopInShopPaymentRequest()
            {
                stamp = Guid.NewGuid().ToString(),
                reference = "9187445",
                amount = 1590,
                currency = "EUR",
                language = "FI",
                orderId = "",
                items = new List<ShopInShopItem>()
            {
                new ShopInShopItem
                {
                    unitPrice = 1590,
                    units = 1,
                    vatPercentage = 24,
                    productCode = "#927502759",
                    category = "Pet supplies",
                    description = "Cat ladder",
                    merchant = MERCHANTIDSISS,
                    stamp = Guid.NewGuid().ToString(),
                    reference = "",
                    orderId = "",
                    commission = new Commission()
                    {
                        merchant = MERCHANTIDSISS,
                        amount = 159
                    }
                }
            }.ToArray(),
                customer = new Customer()
                {
                    email = "erja.esimerkki@example.org",
                    firstName = "Erja",
                    vatId = "FI12345671",
                    companyName = "nothing",
                    lastName = "+358501234567",
                    phone = "123",
                },
                redirectUrls = new CallbackUrl()
                {
                    success = "https://ecom.example.org/success",
                    cancel = "https://ecom.example.org/cancel",
                },

                callbackUrls = new CallbackUrl()
                {
                    success = "https://ecom.example.org/success",
                    cancel = "https://ecom.example.org/cancel"
                },
                deliveryAddress = new Address()
                {
                    city = "Tampere",
                    country = "FI",
                    county = "Pirkanmaa",
                    postalCode = "33100",
                    streetAddress = "Hämeenkatu 6 B"
                },
                invoicingAddress = new Address()
                {
                    city = "Helsinki",
                    country = "FI",
                    county = "Uusimaa",
                    postalCode = "00510",
                    streetAddress = "Testikatu 1"

                },
                groups = new List<string>()
            {
                PaymentMethodGroup.mobile.ToString()
            }.ToArray()

            };
            PaymentResponse res = payTrail.CreateShopInShopPayment(request);
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreatePayment_RequestNull_ReturnCode400()
        {
            //Arrange
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.RequestNull;

            //Act
            PayTrailClient payTrail = new PayTrailClient(MERCHANTIDN, SECRETKEYN, "test");
            PaymentRequest? request = null;
            PaymentResponse res = payTrail.CreatePayment(request);
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreatePayment_ValidateFalse_ReturnCode403()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.ValidateFail;

            //Act
            PayTrailClient payTrail = new PayTrailClient(MERCHANTIDN, SECRETKEYN, "test");
            PaymentRequest request = new PaymentRequest();
            PaymentResponse res = payTrail.CreatePayment(request);
            int actual = res.ReturnCode;
            string a = res.ReturnMessage;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreatePayment_Success_ReturnCode200()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.Success;

            //Act
            PayTrailClient payTrail = new PayTrailClient(MERCHANTIDN, SECRETKEYN, "test");
            PaymentRequest request = new PaymentRequest()
            {
                stamp = Guid.NewGuid().ToString(),
                reference = "9187445",
                amount = 1590,
                currency = "EUR",
                language = "FI",
                orderId = "",
                items = new List<Item>()
            {
                new Item
                {
                    unitPrice = 1590,
                    units = 1,
                    vatPercentage = 24,
                    productCode = "#927502759",
                    category = "Pet supplies",
                    description = "Cat ladder"
                }
            }.ToArray(),
                customer = new Customer()
                {
                    email = "erja.esimerkki@example.org",
                    firstName = "Erja",
                    vatId = "FI12345671",
                    companyName = "nothing",
                    lastName = "+358501234567",
                    phone = "123",
                },
                redirectUrls = new CallbackUrl()
                {
                    success = "https://ecom.example.org/success",
                    cancel = "https://ecom.example.org/cancel",
                },

                callbackUrls = new CallbackUrl()
                {
                    success = "https://ecom.example.org/success",
                    cancel = "https://ecom.example.org/cancel"
                },
                deliveryAddress = new Address()
                {
                    city = "Tampere",
                    country = "FI",
                    county = "Pirkanmaa",
                    postalCode = "33100",
                    streetAddress = "Hämeenkatu 6 B"
                },
                invoicingAddress = new Address()
                {
                    city = "Helsinki",
                    country = "FI",
                    county = "Uusimaa",
                    postalCode = "00510",
                    streetAddress = "Testikatu 1"

                },
                groups = new List<string>()
            {
                PaymentMethodGroup.mobile.ToString()
            }.ToArray()};
            PaymentResponse res = payTrail.CreatePayment(request);
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreatePayment_CallPayTrailReturnNull_ReturnCode404()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.ResponseNull;

            //Act
            PayTrailClient payTrail = new PayTrailClient(MERCHANTIDN, SECRETKEYN, "test");
            PaymentRequest request = new PaymentRequest()
            {
                stamp = Guid.NewGuid().ToString(),
                reference = "9187445",
                amount = 1590,
                currency = "EUR",
                language = "FI",
                orderId = "",
                items = new List<Item>()
            {
                new Item
                {
                    unitPrice = 1590,
                    units = 1,
                    vatPercentage = 24,
                    productCode = "#927502759",
                    category = "Pet supplies",
                    description = "Cat ladder"
                }
            }.ToArray(),
                customer = new Customer()
                {
                    email = "erja.esimerkki@example.org",
                    firstName = "Erja",
                    vatId = "FI12345671",
                    companyName = "nothing",
                    lastName = "+358501234567",
                    phone = "123",
                },
                redirectUrls = new CallbackUrl()
                {
                    success = "https://ecom.example.org/success",
                    cancel = "https://ecom.example.org/cancel",
                },

                callbackUrls = new CallbackUrl()
                {
                    success = "https://ecom.example.org/success",
                    cancel = "https://ecom.example.org/cancel"
                },
                deliveryAddress = new Address()
                {
                    city = "Tampere",
                    country = "FI",
                    county = "Pirkanmaa",
                    postalCode = "33100",
                    streetAddress = "Hämeenkatu 6 B"
                },
                invoicingAddress = new Address()
                {
                    city = "Helsinki",
                    country = "FI",
                    county = "Uusimaa",
                    postalCode = "00510",
                    streetAddress = "Testikatu 1"

                },
                groups = new List<string>()
            {
                PaymentMethodGroup.mobile.ToString()
            }.ToArray()};
            PaymentResponse res = payTrail.CreatePayment(request);
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreatePayment_CallPayTrailReturnFail_ReturnCode500()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.ResponseError;

            //Act
            PayTrailClient payTrail = new PayTrailClient(MERCHANTIDN, SECRETKEYN, "test");
            PaymentRequest request = new PaymentRequest()
            {
                stamp = Guid.NewGuid().ToString(),
                reference = "9187445",
                amount = 1590,
                currency = "EUR",
                language = "FI",
                orderId = "",
                items = new List<ShopInShopItem>()
            {
                new ShopInShopItem
                {
                    unitPrice = 1590,
                    units = 1,
                    vatPercentage = 24,
                    productCode = "#927502759",
                    category = "Pet supplies",
                    description = "Cat ladder",
                    merchant = MERCHANTIDSISS,
                    stamp = Guid.NewGuid().ToString(),
                    reference = "",
                    orderId = ""
                }
            }.ToArray(),
                customer = new Customer()
                {
                    email = "erja.esimerkki@example.org",
                    firstName = "Erja",
                    vatId = "FI12345671",
                    companyName = "nothing",
                    lastName = "+358501234567",
                    phone = "123",
                },
                redirectUrls = new CallbackUrl()
                {
                    success = "https://ecom.example.org/success",
                    cancel = "https://ecom.example.org/cancel",
                },

                callbackUrls = new CallbackUrl()
                {
                    success = "https://ecom.example.org/success",
                    cancel = "https://ecom.example.org/cancel"
                },
                deliveryAddress = new Address()
                {
                    city = "Tampere",
                    country = "FI",
                    county = "Pirkanmaa",
                    postalCode = "33100",
                    streetAddress = "Hämeenkatu 6 B"
                },
                invoicingAddress = new Address()
                {
                    city = "Helsinki",
                    country = "FI",
                    county = "Uusimaa",
                    postalCode = "00510",
                    streetAddress = "Testikatu 1"

                },
                groups = new List<string>()
            {
                PaymentMethodGroup.mobile.ToString()
            }.ToArray()};
            PaymentResponse res = payTrail.CreatePayment(request);
            int actual = res.ReturnCode;
            string a = res.ReturnMessage;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreatePayment_CallPayException_ReturnCode503()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.Exception;

            //Act
            PayTrailClient payTrail = new PayTrailClient(MERCHANTIDN, SECRETKEYN, "test");
            PaymentRequest request = new PaymentRequest()
            {
                stamp = "1222",
                reference = "9187445",
                amount = 1590,
                currency = "EUR",
                language = "FI",
                orderId = "",
                items = new List<Item>()
            {
                new Item
                {
                    unitPrice = 1590,
                    units = 1,
                    vatPercentage = 24,
                    productCode = "#927502759",
                    category = "Pet supplies",
                    description = "Cat ladder"
                }
            }.ToArray(),
                customer = new Customer()
                {
                    email = "erja.esimerkki@example.org",
                    firstName = "Erja",
                    vatId = "FI12345671",
                    companyName = "nothing",
                    lastName = "+358501234567",
                    phone = "123",
                },
                redirectUrls = new CallbackUrl()
                {
                    success = "https://ecom.example.org/success",
                    cancel = "https://ecom.example.org/cancel",
                },

                callbackUrls = new CallbackUrl()
                {
                    success = "https://ecom.example.org/success",
                    cancel = "https://ecom.example.org/cancel"
                },
                deliveryAddress = new Address()
                {
                    city = "Tampere",
                    country = "FI",
                    county = "Pirkanmaa",
                    postalCode = "33100",
                    streetAddress = "Hämeenkatu 6 B"
                },
                invoicingAddress = new Address()
                {
                    city = "Helsinki",
                    country = "FI",
                    county = "Uusimaa",
                    postalCode = "00510",
                    streetAddress = "Testikatu 1"

                },
                groups = new List<string>()
            {
                PaymentMethodGroup.mobile.ToString()
            }.ToArray()};
            PaymentResponse res = payTrail.CreatePayment(request);
            int actual = res.ReturnCode;
            string a = res.ReturnMessage;

            //Assert
            Assert.Equal(expected, actual);
        }
        #endregion Create Payment
    }
}
