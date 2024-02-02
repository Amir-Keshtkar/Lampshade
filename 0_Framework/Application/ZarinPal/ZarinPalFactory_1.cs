using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.Json;
using System.Net.Http.Json;
using System.Reflection;

namespace _0_Framework.Application.ZarinPal {
    public class ZarinPalFactory : IZarinPalFactory {
        private readonly IConfiguration _configuration;

        public string Prefix { get; set; }
        private string MerchantId { get; }

        public ZarinPalFactory(IConfiguration configuration) {
            _configuration = configuration;
            Prefix = _configuration.GetSection("Zibal")["method"];
            MerchantId = _configuration.GetSection("Zibal")["merchant"];
        }

        public PaymentResponse CreatePaymentRequest(string amount, string mobile, string email, string description,
             long orderId) {
            amount = amount.Replace(",", "");
            var finalAmount = int.Parse(amount);
            var siteUrl = _configuration.GetSection("payment")["siteUrl"];

            var _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.DefaultRequestHeaders.Add("cache-control", "no-cache");
            var res = _client.PostAsJsonAsync(Prefix, new {
                Merchant = MerchantId,
                Amount = finalAmount,
                CallbackUrl = $"{siteUrl}Checkout?handler=CallBack&oId={orderId}",
                Description = description,
                OrderId = orderId.ToString(),
                Mobile = mobile,
            }).GetAwaiter().GetResult();
            var stringContent = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var m = JsonConvert.DeserializeObject<MakeRequestResponse>(stringContent);


            return new PaymentResponse() { Authority = m.trackId, Status = m.result };
        }

        public VerificationResponse CreateVerificationRequest(string authority, string amount) {
            var _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _client.DefaultRequestHeaders.Add("cache-control", "no-cache");
            var res = _client.PostAsJsonAsync("https://gateway.zibal.ir/v1/verify", new {
                Merchant = MerchantId,
                TrackId = authority
            }).GetAwaiter().GetResult();
            var stringContent = res.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            var mmm = JsonConvert.DeserializeObject<VerifyResponse>(stringContent);
            return new VerificationResponse() { RefID = long.Parse(mmm.RefNumber ?? "0"), Status = mmm.Status };
        }
    }

    public class VerifyResponse {
        public string PaidAt { get; set; }
        public string CardNumber { get; set; }
        public int Status { get; set; }
        public string Amount { get; set; }
        public string RefNumber { get; set; }
        public string Description { get; set; }
        public int OrderId { get; set; }
        public int Result { get; set; }
        public string Message { get; set; }
    }
    public class MakeRequest {
        public string Merchant { get; set; }
        public string OrderId { get; set; }
        public int Amount { get; set; }
        public string CallbackUrl { get; set; }
        public string Description { get; set; }
        public string Mobile { get; set; }
    }
    public class MakeRequestResponse {
        public string trackId { get; set; }
        public int result { get; set; }
        public string message { get; set; }
        public string payLink { get; set; }
    }
}