using RestSharp;

namespace HPCSAApi.Utils {
    static class RestResponseHandler {
        public static T Handle<T>(RestResponse<T> response) {
            if (response.ErrorException != null)
                throw response.ErrorException.WithContent(response.Content);
            return response.Data;
        }
    }
}
