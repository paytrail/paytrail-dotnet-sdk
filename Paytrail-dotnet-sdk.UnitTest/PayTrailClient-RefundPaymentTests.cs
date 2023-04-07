using Newtonsoft.Json;
using Paytrail_dotnet_sdk.Model.Request;
using Paytrail_dotnet_sdk.Model.Request.RequestModels;
using Paytrail_dotnet_sdk.Model.Response;

namespace Paytrail_dotnet_sdk.UnitTest
{
    public class PayTrailClient_RefundPaymentTests
    {
        const string MERCHANTIDN = "375917";
        const string SECRETKEYN = "SAIPPUAKAUPPIAS";
        const string MERCHANTIDSIS = "695861";
        const string SECRETKEYSIS = "MONISAIPPUAKAUPPIAS";

        [Fact]
        public void RefundPayment_RequestNull_ReturnCode200()
        {
            //Arrange
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.RequestNull;

            //Act
            PayTrailClient payTrail = new PayTrailClient(MERCHANTIDSIS, SECRETKEYSIS, "test");
            string? transactionId = null;
            RefundRequest? refundRequest = null;
            RefundResponse res = payTrail.RefundPayment(refundRequest, transactionId);
            int actual = res.ReturnCode;
            string a = res.ReturnMessage;
            //Assert
            Assert.Equal(expected, actual);
        }


        [Fact]
        public void RefundPayment_Success_ReturnCode1()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.Success;

            //Act
            PayTrailClient ptrail = new PayTrailClient(MERCHANTIDN, SECRETKEYN, "test");
            string transactionId = "4bd78702-c9ea-11ed-beb7-ab93e0fdf0aa";
            List<RefundItem> it = new List<RefundItem>();
            string refundStamp = Guid.NewGuid().ToString();
            RefundRequest refundRequest = new RefundRequest()
            {
                amount = 1590,
                email = "test@gmail.com",
                refundStamp = refundStamp,
                refundReference = "1234",
                callbackUrls = new Model.Request.RequestModels.CallbackUrl
                {
                    cancel = "https://ecom.example.org/refund/cancel",
                    success = "https://ecom.example.org/refund/success"
                },
                items = new RefundItem[0],
            };
            RefundResponse res = ptrail.RefundPayment(refundRequest, transactionId);
            int actual = res.ReturnCode;
            string a = res.ReturnMessage;
            string data = JsonConvert.SerializeObject(res.data);

            //Assert
            Assert.Equal(expected, actual);

        }

        [Fact]
        public void RefundPayment_CallPayTrailReturnNull_ReturnCode300()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.ResponseNull;

            //Act
            PayTrailClient payTrail = new PayTrailClient(MERCHANTIDSIS, SECRETKEYSIS, "test");
            string transactionId = "2f8a77ce-c861-11ed-be51-07513ab7f2f0";
            RefundRequest refundRequest = new RefundRequest(){};

            RefundResponse res = payTrail.RefundPayment(refundRequest, transactionId);

            int actual = res.ReturnCode;
            string a = res.ReturnMessage;
            //Assert
            Assert.Equal(expected, actual);

        }


        [Fact]
        public void RefundPayment_CallPayException_ReturnCode100()
        {
            //Arrage
            int expected = (int)Paytrail_dotnet_sdk.Util.ResponseMessage.Exception;

            //Act
            PayTrailClient payTrail = new PayTrailClient(MERCHANTIDSIS, SECRETKEYSIS, "test");
            string transactionId = "2f8a77ce-c861-11ed-be51-07513ab7f2f0";
            RefundRequest refundRequest = new RefundRequest()
            {
                items = new RefundItem[0],
            };

            RefundResponse res = payTrail.RefundPayment(refundRequest, transactionId);
            int actual = res.ReturnCode;
            string a = res.ReturnMessage;

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
