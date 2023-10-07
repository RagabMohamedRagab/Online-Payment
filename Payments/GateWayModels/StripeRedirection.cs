namespace Payments.GateWayModels {
    public static class StripeRedirection {
        public static string Currency = "EGP";
        public static string SuccessUrl = "https://localhost:44325/Home/Done";
        public static string CancelUrl = "https://localhost:44325/Home/Index";
    }
}
