using System;

namespace HPCSAApi.Utils {
    internal static class ExceptionHelper {
        public const string ExceptionDataKey = "Content";

        public static T WithContent<T>(this T ex, string? content) where T : Exception {
            ex.Data[ExceptionDataKey] = content;
            return ex;
        }
    }
}
