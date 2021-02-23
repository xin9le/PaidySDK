![Banner](https://download.paidy.com/Checkout_728x90.png)


# PaidySDK
PaidySDK is the simple and thin Paidy API wrapper library for .NET that supports following features.

- [Payments API](https://paidy.com/docs/en/payments.html#paidyapi_pay)
- [Tokens API](https://paidy.com/docs/en/tokens.html#paidyapi_tok)
- [Webhooks](https://paidy.com/docs/en/webhook.html)


[![Releases](https://img.shields.io/github/release/xin9le/PaidySDK.svg)](https://github.com/xin9le/PaidySDK/releases)



# Support platforms

- .NET Framework 4.6.1+
- .NET Standard 2.0+
- .NET 5.0+



# How to use

## API

API wrapper services are provided via `Microsoft.Extensions.DependencyInjection`. 


1. `.AddPaidy();`
1. Gets API wrapper service instance via DI.


```cs
using Microsoft.Extensions.DependencyInjection;
using Paidy;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        var options = new PaidyOptions
        (
            apiEndpoint: "https://api.paidy.com",
            secretKey: "sk_...",
            apiVersion: null
        );
        services.AddPaidy(options);
    }
}
```

```cs
using Microsoft.AspNetCore.Mvc;
using Paidy.Payments;
using Paidy.Tokens;

public class SampleController : Controller
{
    private PaymentService PaymentService { get; }
    private TokenService TokenService { get; }

    public SampleController(PaymentService paymentService, TokenService tokenService)
    {
        this.PaymentService = paymentService;
        this.TokenService = tokenService;
    }
}
```



## Webhooks

Allows you to parse the JSON payload that is sent from Paidy webhook easily.

```cs
var payload = "{ Paidy payment webhook payload }"; 
var request = PaymentRequest.From(payload);
```

```cs
var payload = "{ Paidy token webhook payload }"; 
var request = TokenRequest.From(payload);
```



# Installation

Getting started from downloading [NuGet](https://www.nuget.org/packages/PaidySDK) package.

```
dotnet add package PaidySDK
```
```
PM> Install-Package PaidySDK
```



# Paidy docs

- [Japanese](https://paidy.com/docs/jp/)
- [English](https://paidy.com/docs/en/)



# License

This library is provided under [MIT License](http://opensource.org/licenses/MIT).



# Author

Takaaki Suzuki (a.k.a [@xin9le](https://twitter.com/xin9le)) is software developer in Japan who awarded Microsoft MVP for Developer Technologies (C#) since July 2012.
