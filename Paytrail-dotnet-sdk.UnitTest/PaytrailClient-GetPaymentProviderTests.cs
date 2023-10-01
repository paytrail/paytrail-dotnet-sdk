using Paytrail_dotnet_sdk.Model.Request;
using Paytrail_dotnet_sdk.Model.Request.RequestModels;
using Paytrail_dotnet_sdk.Model.Response;

namespace Paytrail_dotnet_sdk.UnitTest
{
    public class PaytrailClient_GetPaymentProviderTests
    {
        const string MERCHANTIDN = "375917";
        const string MERCHANTIDSIS = "695861";
        const string SECRETKEYSIS = "MONISAIPPUAKAUPPIAS";

        #region Get Payment Provider

        [Fact]
        public void GetPaymentProvider_RequestNull_ReturnCode400()
        {
            //Arrange
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.RequestNull;

            //Act
            PaytrailClient payTrail = new PaytrailClient(MERCHANTIDSIS, SECRETKEYSIS, "test");
            GetPaymentProvidersRequest? request = null;
            GetPaymentProvidersResponse res = payTrail.GetPaymentProviders(request);
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetPaymentProvider_ValidateFalse_ReturnCode403()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.ValidateFail;

            //Act
            PaytrailClient payTrail = new PaytrailClient(MERCHANTIDSIS, SECRETKEYSIS, "test");
            GetPaymentProvidersRequest request = new GetPaymentProvidersRequest();
            GetPaymentProvidersResponse res = payTrail.GetPaymentProviders(request);
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetPaymentProvider_Success_ReturnCode200()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.Success;

            //Act
            PaytrailClient payTrail = new PaytrailClient(MERCHANTIDSIS, SECRETKEYSIS, "test");

            GetPaymentProvidersRequest request = new GetPaymentProvidersRequest
            {
                Amount = 100,
                Groups = new List<PaymentMethodGroup>()
                {
                    PaymentMethodGroup.creditcard,
                }
            };

            GetPaymentProvidersResponse res = payTrail.GetPaymentProviders(request);
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetGroupPaymentProvider_CallPaytrailReturnFail_ReturnCode500()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.ResponseError;

            //Act
            PaytrailClient payTrail = new PaytrailClient(MERCHANTIDN, SECRETKEYSIS, "test");
            GetPaymentProvidersRequest request = new GetPaymentProvidersRequest
            {
                Amount = 100,
                Groups = new List<PaymentMethodGroup>()
                {
                    PaymentMethodGroup.creditcard,
                },
            };

            GetPaymentProvidersResponse res = payTrail.GetPaymentProviders(request);
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }

        #endregion Get Payment Provider
    }
}
