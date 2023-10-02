# Paytrail .NET SDK

.NET Software Development Kit for [Paytrail](https://www.paytrail.com/) payment service

## Paytrail Payment Service

Paytrail is a payment gateway that offer 20+ payment methods for finish customers.

The payment gateway provides all the popular payment methods with one simple integration. The provided payment methods include, but are not limited to, credit cards, online banking and mobile payments.

To use the payment service, you need to sign up for a Paytrai account. Transactio fees will be charged for every transaction. Transaction cost may vary from merchant to merchant, based on what is agreed upon with Paytrail when negotiating your contract. For more informationm and registration, please visit our [website](https://www.paytrail.com/) or contact (asiakaspalvelu@paytrail.com) directly.

## Requirements

### General requirements

- .NET and .NET Core 2.0 or later
- .NET Framework 4.6.1 or later

### Development requirements

- [XUnit](https://xunit.net/) - community-focused unit testing tool for the .NET

## Installation

Install with Nuget Package Management
Install with .NET Core CLI

```
Nuget\Install-Package Paytrail-dotnet-sdk -Version 1.0.2
```

Install with Package Manager Console`

```
dotnet add package Paytrail-dotnet-sdk --version 1.0.2
```

## Usage

```c#
using Newtonsoft.Json;
using Paytrail_dotnet_sdk;
using Paytrail_dotnet_sdk.Model.Request;
using Paytrail_dotnet_sdk.Model.Request.RequestModels;
using Paytrail_dotnet_sdk.Model.Response;

class Program
{
    static void Main()
    {
        PayTrailClient payTrail = new PayTrailClient("123456", "xxx", "xxx");

        GetPaymentProvidersRequest payload = new GetPaymentProvidersRequest
        {
            amount = 1000,
            groups = new List<PaymentMethodGroup>()
            {
                PaymentMethodGroup.creditcard,
            }
        };

        GetPaymentProvidersResponse res = payTrail.GetPaymentProviders(payload);

        Console.WriteLine(JsonConvert.SerializeObject(res));
    }
}
```

## Folder contents & descriptions

| Folder/File                           | Content/Description                                |
| ------------------------------------- | -------------------------------------------------- |
| Paytrail-dotnet-sdk/Interface         | Interface for all the related classes to implement |
| Paytrail-dotnet-sdk/Model             | Model classes and functions                        |
| Paytrail-dotnet-sdk/Model/Request     | Request model and functions                        |
| Paytrail-dotnet-sdk/Model/Response    | Response model and functions                       |
| Paytrail-dotnet-sdk/Util              | Utility/Enum classes and functions                 |
| Paytrail-dotnet-sdk/Paytrail.cs       | Init paytrail service                              |
| Paytrail-dotnet-sdk/PaytrailClient.cs | Paytrail client class and functions                |
| Paytrail-dotnet-sdk.UnitTest          | .NET unit test                                     |

## Basic fucntionalities

The Paytrail-dotnet-sdk supports some functionalities of the [Paytrail Payment API](https://docs.paytrail.com/#/).

Some of the key features are:

### Payments and refunds

- [Creating payment request](https://docs.paytrail.com/#/?id=create)
- [Creating payment status request](https://docs.paytrail.com/#/?id=get)
- [Creating refund request](https://docs.paytrail.com/#/?id=refund)

### Shop-in-shop

- Creating Shop-in-shop payment request

### Token payments

- [Pay and add card](https://docs.paytrail.com/#/?id=pay-and-add-card)

## Methods

| Method                              | Description                                                   |
| ----------------------------------- | ------------------------------------------------------------- |
| CreatePayment()                     | Create payment                                                |
| CreateShopInShopPayment()           | Create SiS payment                                            |
| GetPaymentInfo()                    | Request payment status                                        |
| RefundPayment()                     | Create refund request                                         |
| PayAndAddCardRequest()              | Combine a payment and adding a new card with a single request |
| GetPaymentProviders()               | Get a list of payment providers                               |
| GetGroupedPaymentProviders()        | Returns an array of grouped payment providers fields          |
| EmailRefund()                       | Create email refund                                           |
| CreateGetTokenRequest()             | Request card token                                            |
| GetSettlements()                    | Request settlements                                           |
| RequestPaymentReport()              | Request payment report                                        |
| CreateMitPaymentCharge()            | Create MiT payment                                            |
| CreateMitPaymentAuthorizationHold() | Create MiT authorization hold                                 |
| CreateCitPaymentCharge()            | Create CiT payment                                            |
| CreateCitPaymentAuthorizationHold() | Create CiT authorization hold                                 |
| CreateMitPaymentCommit()            | Commit MiT authorization hold                                 |
| CreateCitPaymentCommit()            | Commit CiT authorization hold                                 |
| RevertPaymentAuthorizationHold()    | Revert existing Mit or CiT authorization hold                 |
| RequestPaymentReportBySettlement()  | Request payment report by settlement ID                       |
| CreateAddCardFormRequest()          | Save card details                                             |
