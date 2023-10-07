using Payments.GateWayModels;

namespace Payments.Gateway {
    public interface IStripeGateWay {
        Task<string> OnlinePaymentStripe(Amount amount);
    }
}
