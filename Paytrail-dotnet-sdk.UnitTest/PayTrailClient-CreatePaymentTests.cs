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
                Stamp = Guid.NewGuid().ToString(),
                Reference = "9187445",
                Amount = 1590,
                Currency = "EUR",
                Language = "FI",
                OrderId = "",
                Items = new List<ShopInShopItem>()
            {
                new ShopInShopItem
                {
                    UnitPrice = 1590,
                    Units = 1,
                    VatPercentage = 24,
                    ProductCode = "#927502759",
                    Category = "Pet supplies",
                    Description = "Cat ladder",
                    Merchant = MERCHANTIDSISS,
                    Stamp = Guid.NewGuid().ToString(),
                    Reference = "",
                    OrderId = "",
                    Commission = new Commission()
                    {
                        Merchant = MERCHANTIDSISS,
                        Amount = 159
                    }
                }
            }.ToArray(),
                Customer = new Customer()
                {
                    Email = "erja.esimerkki@example.org",
                    FirstName = "Erja",
                    VatId = "FI12345671",
                    CompanyName = "nothing",
                    LastName = "+358501234567",
                    Phone = "123",
                },
                RedirectUrls = new CallbackUrl()
                {
                    Success = "https://ecom.example.org/success",
                    Cancel = "https://ecom.example.org/cancel",
                },

                CallbackUrls = new CallbackUrl()
                {
                    Success = "https://ecom.example.org/success",
                    Cancel = "https://ecom.example.org/cancel"
                },
                DeliveryAddress = new Address()
                {
                    City = "Tampere",
                    Country = "FI",
                    County = "Pirkanmaa",
                    PostalCode = "33100",
                    StreetAddress = "Hämeenkatu 6 B"
                },
                InvoicingAddress = new Address()
                {
                    City = "Helsinki",
                    Country = "FI",
                    County = "Uusimaa",
                    PostalCode = "00510",
                    StreetAddress = "Testikatu 1"

                },
                Groups = new List<string>()
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
                Stamp = Guid.NewGuid().ToString(),
                Reference = "9187445",
                Amount = 1590,
                Currency = "EUR",
                Language = "FI",
                OrderId = "",
                Items = new List<ShopInShopItem>()
            {
                new ShopInShopItem
                {
                    OrderId = "",
                    UnitPrice = 1590,
                    Units = 1,
                    VatPercentage = 24,
                    ProductCode = "#927502759",
                    Category = "Pet supplies",
                    Description = "Cat ladder",
                    Merchant = MERCHANTIDSISS,
                    Stamp = Guid.NewGuid().ToString(),
                    Reference = "",
                    Commission = new Commission()
                    {
                        Merchant = MERCHANTIDSISS,
                        Amount = 159
                    }
                }
            }.ToArray(),
                Customer = new Customer()
                {
                    Email = "erja.esimerkki@example.org",
                    FirstName = "Erja",
                    VatId = "FI12345671",
                    CompanyName = "nothing",
                    LastName = "+358501234567",
                    Phone = "123",
                },
                RedirectUrls = new CallbackUrl()
                {
                    Success = "https://ecom.example.org/success",
                    Cancel = "https://ecom.example.org/cancel",
                },

                CallbackUrls = new CallbackUrl()
                {
                    Success = "https://ecom.example.org/success",
                    Cancel = "https://ecom.example.org/cancel"
                },
                DeliveryAddress = new Address()
                {
                    City = "Tampere",
                    Country = "FI",
                    County = "Pirkanmaa",
                    PostalCode = "33100",
                    StreetAddress = "Hämeenkatu 6 B"
                },
                InvoicingAddress = new Address()
                {
                    City = "Helsinki",
                    Country = "FI",
                    County = "Uusimaa",
                    PostalCode = "00510",
                    StreetAddress = "Testikatu 1"

                },
                Groups = new List<string>()
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
                Stamp = Guid.NewGuid().ToString(),
                Reference = "9187445",
                Amount = 1590,
                Currency = "EUR",
                Language = "FI",
                OrderId = "",
                Items = new List<ShopInShopItem>()
            {
                new ShopInShopItem
                {
                    UnitPrice = 1590,
                    Units = 1,
                    VatPercentage = 24,
                    ProductCode = "#927502759",
                    Category = "Pet supplies",
                    Description = "Cat ladder",
                    Merchant = MERCHANTIDSISS,
                    Stamp = Guid.NewGuid().ToString(),
                    Reference = ""
                }
            }.ToArray(),
                Customer = new Customer()
                {
                    Email = "erja.esimerkki@example.org",
                    FirstName = "Erja",
                    VatId = "FI12345671",
                    CompanyName = "nothing",
                    LastName = "+358501234567",
                    Phone = "123",
                },
                RedirectUrls = new CallbackUrl()
                {
                    Success = "https://ecom.example.org/success",
                    Cancel = "https://ecom.example.org/cancel",
                },

                CallbackUrls = new CallbackUrl()
                {
                    Success = "https://ecom.example.org/success",
                    Cancel = "https://ecom.example.org/cancel"
                },
                DeliveryAddress = new Address()
                {
                    City = "Tampere",
                    Country = "FI",
                    County = "Pirkanmaa",
                    PostalCode = "33100",
                    StreetAddress = "Hämeenkatu 6 B"
                },
                InvoicingAddress = new Address()
                {
                    City = "Helsinki",
                    Country = "FI",
                    County = "Uusimaa",
                    PostalCode = "00510",
                    StreetAddress = "Testikatu 1"

                },
                Groups = new List<string>()
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
                Stamp = Guid.NewGuid().ToString(),
                Reference = "9187445",
                Amount = 1590,
                Currency = "EUR",
                Language = "FI",
                OrderId = "",
                Items = new List<ShopInShopItem>()
            {
                new ShopInShopItem
                {
                    UnitPrice = 1590,
                    Units = 1,
                    VatPercentage = 24,
                    ProductCode = "#927502759",
                    Category = "Pet supplies",
                    Description = "Cat ladder",
                    Merchant = MERCHANTIDSISS,
                    Stamp = Guid.NewGuid().ToString(),
                    Reference = "",
                    OrderId = "",
                    Commission = new Commission()
                    {
                        Merchant = MERCHANTIDSISS,
                        Amount = 159
                    }
                }
            }.ToArray(),
                Customer = new Customer()
                {
                    Email = "erja.esimerkki@example.org",
                    FirstName = "Erja",
                    VatId = "FI12345671",
                    CompanyName = "nothing",
                    LastName = "+358501234567",
                    Phone = "123",
                },
                RedirectUrls = new CallbackUrl()
                {
                    Success = "https://ecom.example.org/success",
                    Cancel = "https://ecom.example.org/cancel",
                },

                CallbackUrls = new CallbackUrl()
                {
                    Success = "https://ecom.example.org/success",
                    Cancel = "https://ecom.example.org/cancel"
                },
                DeliveryAddress = new Address()
                {
                    City = "Tampere",
                    Country = "FI",
                    County = "Pirkanmaa",
                    PostalCode = "33100",
                    StreetAddress = "Hämeenkatu 6 B"
                },
                InvoicingAddress = new Address()
                {
                    City = "Helsinki",
                    Country = "FI",
                    County = "Uusimaa",
                    PostalCode = "00510",
                    StreetAddress = "Testikatu 1"

                },
                Groups = new List<string>()
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
                Stamp = Guid.NewGuid().ToString(),
                Reference = "9187445",
                Amount = 1590,
                Currency = "EUR",
                Language = "FI",
                OrderId = "",
                Items = new List<Item>()
            {
                new Item
                {
                    UnitPrice = 1590,
                    Units = 1,
                    VatPercentage = 24,
                    ProductCode = "#927502759",
                    Category = "Pet supplies",
                    Description = "Cat ladder"
                }
            }.ToArray(),
                Customer = new Customer()
                {
                    Email = "erja.esimerkki@example.org",
                    FirstName = "Erja",
                    VatId = "FI12345671",
                    CompanyName = "nothing",
                    LastName = "+358501234567",
                    Phone = "123",
                },
                RedirectUrls = new CallbackUrl()
                {
                    Success = "https://ecom.example.org/success",
                    Cancel = "https://ecom.example.org/cancel",
                },

                CallbackUrls = new CallbackUrl()
                {
                    Success = "https://ecom.example.org/success",
                    Cancel = "https://ecom.example.org/cancel"
                },
                DeliveryAddress = new Address()
                {
                    City = "Tampere",
                    Country = "FI",
                    County = "Pirkanmaa",
                    PostalCode = "33100",
                    StreetAddress = "Hämeenkatu 6 B"
                },
                InvoicingAddress = new Address()
                {
                    City = "Helsinki",
                    Country = "FI",
                    County = "Uusimaa",
                    PostalCode = "00510",
                    StreetAddress = "Testikatu 1"

                },
                Groups = new List<string>()
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
                Stamp = Guid.NewGuid().ToString(),
                Reference = "9187445",
                Amount = 1590,
                Currency = "EUR",
                Language = "FI",
                OrderId = "",
                Items = new List<Item>()
            {
                new Item
                {
                    UnitPrice = 1590,
                    Units = 1,
                    VatPercentage = 24,
                    ProductCode = "#927502759",
                    Category = "Pet supplies",
                    Description = "Cat ladder"
                }
            }.ToArray(),
                Customer = new Customer()
                {
                    Email = "erja.esimerkki@example.org",
                    FirstName = "Erja",
                    VatId = "FI12345671",
                    CompanyName = "nothing",
                    LastName = "+358501234567",
                    Phone = "123",
                },
                RedirectUrls = new CallbackUrl()
                {
                    Success = "https://ecom.example.org/success",
                    Cancel = "https://ecom.example.org/cancel",
                },

                CallbackUrls = new CallbackUrl()
                {
                    Success = "https://ecom.example.org/success",
                    Cancel = "https://ecom.example.org/cancel"
                },
                DeliveryAddress = new Address()
                {
                    City = "Tampere",
                    Country = "FI",
                    County = "Pirkanmaa",
                    PostalCode = "33100",
                    StreetAddress = "Hämeenkatu 6 B"
                },
                InvoicingAddress = new Address()
                {
                    City = "Helsinki",
                    Country = "FI",
                    County = "Uusimaa",
                    PostalCode = "00510",
                    StreetAddress = "Testikatu 1"

                },
                Groups = new List<string>()
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
                Stamp = Guid.NewGuid().ToString(),
                Reference = "9187445",
                Amount = 1590,
                Currency = "EUR",
                Language = "FI",
                OrderId = "",
                Items = new List<ShopInShopItem>()
            {
                new ShopInShopItem
                {
                    UnitPrice = 1590,
                    Units = 1,
                    VatPercentage = 24,
                    ProductCode = "#927502759",
                    Category = "Pet supplies",
                    Description = "Cat ladder",
                    Merchant = MERCHANTIDSISS,
                    Stamp = Guid.NewGuid().ToString(),
                    Reference = "",
                    OrderId = ""
                }
            }.ToArray(),
                Customer = new Customer()
                {
                    Email = "erja.esimerkki@example.org",
                    FirstName = "Erja",
                    VatId = "FI12345671",
                    CompanyName = "nothing",
                    LastName = "+358501234567",
                    Phone = "123",
                },
                RedirectUrls = new CallbackUrl()
                {
                    Success = "https://ecom.example.org/success",
                    Cancel = "https://ecom.example.org/cancel",
                },

                CallbackUrls = new CallbackUrl()
                {
                    Success = "https://ecom.example.org/success",
                    Cancel = "https://ecom.example.org/cancel"
                },
                DeliveryAddress = new Address()
                {
                    City = "Tampere",
                    Country = "FI",
                    County = "Pirkanmaa",
                    PostalCode = "33100",
                    StreetAddress = "Hämeenkatu 6 B"
                },
                InvoicingAddress = new Address()
                {
                    City = "Helsinki",
                    Country = "FI",
                    County = "Uusimaa",
                    PostalCode = "00510",
                    StreetAddress = "Testikatu 1"

                },
                Groups = new List<string>()
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
                Stamp = "1222",
                Reference = "9187445",
                Amount = 1590,
                Currency = "EUR",
                Language = "FI",
                OrderId = "",
                Items = new List<Item>()
            {
                new Item
                {
                    UnitPrice = 1590,
                    Units = 1,
                    VatPercentage = 24,
                    ProductCode = "#927502759",
                    Category = "Pet supplies",
                    Description = "Cat ladder"
                }
            }.ToArray(),
                Customer = new Customer()
                {
                    Email = "erja.esimerkki@example.org",
                    FirstName = "Erja",
                    VatId = "FI12345671",
                    CompanyName = "nothing",
                    LastName = "+358501234567",
                    Phone = "123",
                },
                RedirectUrls = new CallbackUrl()
                {
                    Success = "https://ecom.example.org/success",
                    Cancel = "https://ecom.example.org/cancel",
                },

                CallbackUrls = new CallbackUrl()
                {
                    Success = "https://ecom.example.org/success",
                    Cancel = "https://ecom.example.org/cancel"
                },
                DeliveryAddress = new Address()
                {
                    City = "Tampere",
                    Country = "FI",
                    County = "Pirkanmaa",
                    PostalCode = "33100",
                    StreetAddress = "Hämeenkatu 6 B"
                },
                InvoicingAddress = new Address()
                {
                    City = "Helsinki",
                    Country = "FI",
                    County = "Uusimaa",
                    PostalCode = "00510",
                    StreetAddress = "Testikatu 1"

                },
                Groups = new List<string>()
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
