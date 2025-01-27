using RestSharp;

namespace HPCSAApi {
    public interface IHPCSAApiClient {
    }

    public class HPCSAApiClient : IHPCSAApiClient {
        private readonly RestClient client;

        public HPCSAApiClient() {
            client = new RestClient("https://hpcsaonline.custhelp.com");
            client.AddDefaultHeader("Accept", "application/json");
        }
    }
}
