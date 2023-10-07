using Microsoft.Extensions.Options;
using Payments.GateWayModels;
using Stripe;
using Stripe.Checkout;

namespace Payments.Gateway {
    public class StripeGateWay : IStripeGateWay {
        private readonly StripeSettings _stripeSettings;

        public StripeGateWay(IOptions<StripeSettings> stripeSettings)
        {
            _stripeSettings = stripeSettings.Value;
        }

        public async Task<string> OnlinePaymentStripe(Amount amount)
        {
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

            var Options = new SessionCreateOptions()
            {
                PaymentMethodTypes = new List<string>()
                {
                    "card"
                },
                LineItems = new List<SessionLineItemOptions>()
                {
                    new SessionLineItemOptions()
                    {
                        PriceData=new SessionLineItemPriceDataOptions()
                        {
                            Currency=StripeRedirection.Currency,
                            UnitAmount=   amount.TotalPrice,
                            ProductData=new SessionLineItemPriceDataProductDataOptions()
                            {
                                Name="T-Shirt",
                                 Description="Excellent T-Shirt"
                            }                                                
                        },
                        Quantity=20
                    }
                }  ,
                Mode= "payment",
                SuccessUrl=StripeRedirection.SuccessUrl,
                CancelUrl=StripeRedirection.CancelUrl
            };
            var Service = new SessionService();
            var Session =await Service.CreateAsync(Options);
            return Session.Url;
        }
    }
}
