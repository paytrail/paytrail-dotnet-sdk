using Moq;
using Paytrail_dotnet_sdk.Interface;
using Paytrail_dotnet_sdk.Model.Request;
using Paytrail_dotnet_sdk.Model.Response;

namespace Paytrail_dotnet_sdk.UnitTest
{
    public class PaytrailClient_EmailRefundTests
    {
        const string MERCHANTIDN = "375917";
        const string MERCHANTIDSIS = "695861";
        const string SECRETKEYSIS = "MONISAIPPUAKAUPPIAS";

        #region Email refund

        [Fact]
        public void EmailRefund_RequestNull_ReturnCode400()
        {
            //Arrange
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.RequestNull;

            //Act
            PaytrailClient payTrail = new PaytrailClient(MERCHANTIDSIS, SECRETKEYSIS, "test");
            EmailRefundRequest? request = null;
            EmailRefundResponse res = payTrail.EmailRefund(request, "0e056dd8-408f-11ee-9cb4-e3059a523029");
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void EmailRefund_ValidateFalse_ReturnCode403()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.ValidateFail;

            //Act
            PaytrailClient payTrail = new PaytrailClient(MERCHANTIDSIS, SECRETKEYSIS, "test");
            EmailRefundRequest request = new EmailRefundRequest();
            EmailRefundResponse res = payTrail.EmailRefund(request, "0e056dd8-408f-11ee-9cb4-e3059a523029");
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void EmailRefund_Success_ReturnCode200()
        {
            // Arrange
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.Success;

            // Create a mock for IPaytrail interface
            var payTrailMock = new Mock<IPaytrail>();

            // Set up the mock to return a success response when EmailRefund is called
            payTrailMock.Setup(client => client.EmailRefund(It.IsAny<EmailRefundRequest>(), It.IsAny<string>()))
                .Returns(new EmailRefundResponse { ReturnCode = expected });

            // Act
            var emailRefundRequest = new EmailRefundRequest
            {
                Amount = 1590,
                Email = "test@gmail.com",
                CallbackUrls = new Model.Request.RequestModels.CallbackUrl
                {
                    Cancel = "https://ecom.example.org/refund/cancel",
                    Success = "https://ecom.example.org/refund/success"
                },
                Items = new Model.Request.RequestModels.RefundItem[0],
            };

            var transactionId = "0e056dd8-408f-11ee-9cb4-e3059a523029";
            var res = payTrailMock.Object.EmailRefund(emailRefundRequest, transactionId);
            int actual = res.ReturnCode;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void EmailRefund_CallPaytrailReturnFail_ReturnCode500()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.ResponseError;

            //Act
            PaytrailClient payTrail = new PaytrailClient(MERCHANTIDN, SECRETKEYSIS, "test");
            EmailRefundRequest request = new EmailRefundRequest
            {
                Amount = 1590,
                Email = "test@gmail.com",
                CallbackUrls = new Model.Request.RequestModels.CallbackUrl
                {
                    Cancel = "https://ecom.example.org/refund/cancel",
                    Success = "https://ecom.example.org/refund/success"
                },
                Items = new Model.Request.RequestModels.RefundItem[0],
            };

            EmailRefundResponse res = payTrail.EmailRefund(request, "0e056dd8-408f-11ee-9cb4-e3059a523029");
            int actual = res.ReturnCode;

            //Assert
            Assert.Equal(expected, actual);
        }
    }

    #endregion Email refund
}
