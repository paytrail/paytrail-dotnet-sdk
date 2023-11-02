using Paytrail_dotnet_sdk.Model.Request;
using Paytrail_dotnet_sdk.Model.Response;

namespace Paytrail_dotnet_sdk.UnitTest
{
    public class PaytrailClient_CreateAddCardFormRequestTests
    {
        const string MERCHANTIDN = "375917";
        const string MERCHANTIDSIS = "695861";
        const string SECRETKEYSIS = "MONISAIPPUAKAUPPIAS";

        #region Create Add Card Form Request

        [Fact]
        public void CreateAddCardFormRequest_RequestNull_ReturnCode400()
        {
            //Arrange
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.RequestNull;

            //Act
            PaytrailClient payTrail = new PaytrailClient(MERCHANTIDSIS, SECRETKEYSIS, "test");
            AddCardFormRequest? request = null;
            AddCardFormResponse res = payTrail.CreateAddCardFormRequest(request);
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateAddCardFormRequest_ValidateFalse_ReturnCode403()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.ValidateFail;

            //Act
            PaytrailClient payTrail = new PaytrailClient(MERCHANTIDSIS, SECRETKEYSIS, "test");
            AddCardFormRequest request = new AddCardFormRequest();
            AddCardFormResponse res = payTrail.CreateAddCardFormRequest(request);
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateAddCardFormRequest_Success_ReturnCode200()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.Success;

            //Act
            PaytrailClient payTrail = new PaytrailClient(MERCHANTIDSIS, SECRETKEYSIS, "test");

            AddCardFormRequest request = new AddCardFormRequest
            {
                CheckoutAccount = 375917,
                CheckoutAlgorithm = "sha256",
                CheckoutMethod = "POST",
                CheckoutNonce = "6501220b16b7",
                CheckoutTimestamp = "2023-08-22T04:05:20.253Z",
                CheckoutRedirectSuccessUrl = "https://somedomain.com/success",
                CheckoutRedirectCancelUrl = "https://somedomain.com/cancel",
                Signature = "542e780c253761ed64333d5485391ddd4f55d5e00b7bdc7f60f0f0d15516f889",
                Language = "EN"
            };

            AddCardFormResponse res = payTrail.CreateAddCardFormRequest(request);
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }
    }

    #endregion Add Card Form Request
}
