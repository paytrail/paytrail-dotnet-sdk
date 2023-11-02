using Paytrail_dotnet_sdk.Model.Request;
using Paytrail_dotnet_sdk.Model.Request.RequestModels;
using Paytrail_dotnet_sdk.Model.Response;

namespace Paytrail_dotnet_sdk.UnitTest
{
    public class PaytrailClient_CreateMitPaymentAuthorizationHoldTests
    {
        const string MERCHANTIDN = "375917";
        const string MERCHANTIDSIS = "695861";
        const string SECRETKEYSIS = "SAIPPUAKAUPPIAS";

        #region Create Mit Payment Authorization Hold

        [Fact]
        public void CreateMitPaymentAuthorizationHold_RequestNull_ReturnCode400()
        {
            //Arrange
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.RequestNull;

            //Act
            PaytrailClient payTrail = new PaytrailClient(MERCHANTIDN, SECRETKEYSIS, "test");
            CreateMitOrCitPaymentRequest? request = null;
            CreateMitOrCitPaymentResponse res = payTrail.CreateMitPaymentAuthorizationHold(request, "0e056dd8-408f-11ee-9cb4-e3059a523029");
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateMitPaymentAuthorizationHold_ValidateFalse_ReturnCode403()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.ValidateFail;

            //Act
            PaytrailClient payTrail = new PaytrailClient(MERCHANTIDN, SECRETKEYSIS, "test");
            CreateMitOrCitPaymentRequest request = new CreateMitOrCitPaymentRequest();
            CreateMitOrCitPaymentResponse res = payTrail.CreateMitPaymentAuthorizationHold(request, "0e056dd8-408f-11ee-9cb4-e3059a523029");
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateMitPaymentAuthorizationHold_Success_ReturnCode200()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.Success;

            //Act
            PaytrailClient payTrail = new PaytrailClient(MERCHANTIDN, SECRETKEYSIS, "test");

            CreateMitOrCitPaymentRequest payload = new CreateMitOrCitPaymentRequest
            {
                Token = "c7441208-c2a1-4a10-8eb6-458bd8eaa64f",
                Stamp = Guid.NewGuid().ToString(),
                Reference = "9187445",
                Amount = 1590,
                Currency = "EUR",
                Language = "FI",
                OrderId = "", //Order ID. Used for e.g. Walley/Collector payments order ID. If not given, merchant reference is used instead.
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
                        Stamp = Guid.NewGuid().ToString(),
                        Reference = "9187445",
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

            CreateMitOrCitPaymentResponse res = payTrail.CreateMitPaymentAuthorizationHold(payload, "0e056dd8-408f-11ee-9cb4-e3059a523029");
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateMitPaymentAuthorizationHold_CallPaytrailReturnFail_ReturnCode500()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.ResponseError;

            //Act
            PaytrailClient payTrail = new PaytrailClient(MERCHANTIDSIS, SECRETKEYSIS, "test");

            CreateMitOrCitPaymentRequest payload = new CreateMitOrCitPaymentRequest
            {
                Token = "c7441208-c2a1-4a10-8eb6-458bd8eaa64f",
                Stamp = Guid.NewGuid().ToString(),
                Reference = "9187445",
                Amount = 1590,
                Currency = "EUR",
                Language = "FI",
                OrderId = "", //Order ID. Used for e.g. Walley/Collector payments order ID. If not given, merchant reference is used instead.
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
                        Stamp = Guid.NewGuid().ToString(),
                        Reference = "9187445",
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

            CreateMitOrCitPaymentResponse res = payTrail.CreateMitPaymentAuthorizationHold(payload, "0e056dd8-408f-11ee-9cb4-e3059a523029");
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }
    }

    #endregion Create Mit Payment Authorization Hold
}
