using RestSharp;

namespace HPCSAApi.Utils {
    internal static class RestResponseHandler {
        public static T Handle<T>(RestResponse<T> response) {
            if (response.ErrorException != null)
                throw response.ErrorException.WithContent(response.Content);
            if (response.Data == null)
                throw new System.ApplicationException("Empty Data!").WithContent(response.Content);
            return response.Data;
        }
    }
}
