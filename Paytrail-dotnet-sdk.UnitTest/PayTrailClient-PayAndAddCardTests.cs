using Paytrail_dotnet_sdk.Model.Request;
using Paytrail_dotnet_sdk.Model.Request.RequestModels;
using Paytrail_dotnet_sdk.Model.Response;


namespace Paytrail_dotnet_sdk.UnitTest
{
    public class PayTrailClient_PayAndAddCardTests
    {
        const string MERCHANTIDN = "375917";
        const string SECRETKEYN = "SAIPPUAKAUPPIAS";
        const string MERCHANTIDSISS = "695874";

        #region Pay and add card

        [Fact]
        public void PayAndAddCard_RequestNull_ReturnCode400()
        {
            //Arrange
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.RequestNull;

            //Act
            PayTrailClient payTrail = new PayTrailClient(MERCHANTIDN, SECRETKEYN, "test");
            PayAddCardRequest? request = null;
            PayAddCardResponse res = payTrail.PayAndAddCard(request);
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PayAndAddCard_ValidateFalse_ReturnCode403()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.ValidateFail;

            //Act
            PayTrailClient payTrail = new PayTrailClient(MERCHANTIDN, SECRETKEYN, "test");
            PayAddCardRequest request = new PayAddCardRequest();
            PayAddCardResponse res = payTrail.PayAndAddCard(request);
            int actual = res.ReturnCode;
            string a = res.ReturnMessage;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PayAndAddCard_Success_ReturnCode200()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.Success;

            //Act
            PayTrailClient payTrail = new PayTrailClient(MERCHANTIDN, SECRETKEYN, "test");
            PayAddCardRequest request = new PayAddCardRequest()
            {
                Stamp = Guid.NewGuid().ToString(),
                Reference = "9187445",
                Amount = 1590,
                Currency = "EUR",
                Language = "FI",
                OrderId = "079599516354",
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
            }.ToArray()
            };
            PayAddCardResponse res = payTrail.PayAndAddCard(request);
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PayAndAddCard_CallPayTrailReturnNull_ReturnCode404()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.ResponseNull;

            //Act
            PayTrailClient payTrail = new PayTrailClient(MERCHANTIDN, SECRETKEYN, "test");
            PayAddCardRequest request = new PayAddCardRequest()
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
            }.ToArray()
            };
            PayAddCardResponse res = payTrail.PayAndAddCard(request);
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PayAndAddCard_CallPayTrailReturnFail_ReturnCode500()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.ResponseError;

            //Act
            PayTrailClient payTrail = new PayTrailClient(MERCHANTIDN, SECRETKEYN, "test");
            PayAddCardRequest request = new PayAddCardRequest()
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
            }.ToArray()
            };
            PayAddCardResponse res = payTrail.PayAndAddCard(request);
            int actual = res.ReturnCode;
            string a = res.ReturnMessage;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void PayAndAddCard_CallPayException_ReturnCode503()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.Exception;

            //Act
            PayTrailClient payTrail = new PayTrailClient(MERCHANTIDN, SECRETKEYN, "test");
            PayAddCardRequest request = new PayAddCardRequest()
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
            }.ToArray()
            };
            PayAddCardResponse res = payTrail.PayAndAddCard(request);
            int actual = res.ReturnCode;
            string a = res.ReturnMessage;

            //Assert
            Assert.Equal(expected, actual);
        }
        #endregion Pay and add card
    }
}
