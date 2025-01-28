using HPCSAApi.Actions;
using RestSharp;

namespace HPCSAApi {
    public interface IHPCSAApiClient {
        IRegisterSearch RegisterSearch { get; }
    }

    public class HPCSAApiClient : IHPCSAApiClient {
        private readonly RestClient client;

        public HPCSAApiClient() {
            client = new RestClient("https://hpcsaonline.custhelp.com");
            client.AddDefaultHeader("Accept", "application/json");
        }

        public IRegisterSearch RegisterSearch => new RegisterSearch(client);
    }
}
