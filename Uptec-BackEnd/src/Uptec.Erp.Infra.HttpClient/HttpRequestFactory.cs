using System.Net.Http;
using System.Threading.Tasks;

namespace Uptec.Erp.Infra.HttpClient
{
    public static class HttpRequestFactory
    {
        public static async Task<HttpResponseMessage> Get(string requestUri)
            => await Get(requestUri, "");

        public static async Task<HttpResponseMessage> Get(string requestUri, string bearerToken)
        {
            var builder = new HttpRequestBuilder()
                                .SetMethod(HttpMethod.Get)
                                .SetRequestUri(requestUri)
                                .SetBearerToken(bearerToken);

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Post(string requestUri, object value) 
            => await Post(requestUri, value, "");

        public static async Task<HttpResponseMessage> Post(string requestUri, object value, string bearerToken)
        {
            var builder = new HttpRequestBuilder()
                                .SetMethod(HttpMethod.Post)
                                .SetRequestUri(requestUri)
                                .SetContent(new JsonContent(value))
                                .SetBearerToken(bearerToken);

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Put(string requestUri, object value)
            => await Put(requestUri, value, "");

        public static async Task<HttpResponseMessage> Put(
            string requestUri, object value, string bearerToken)
        {
            var builder = new HttpRequestBuilder()
                                .SetMethod(HttpMethod.Put)
                                .SetRequestUri(requestUri)
                                .SetContent(new JsonContent(value))
                                .SetBearerToken(bearerToken);

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Patch(string requestUri, object value)
            => await Patch(requestUri, value, "");

        public static async Task<HttpResponseMessage> Patch(
            string requestUri, object value, string bearerToken)
        {
            var builder = new HttpRequestBuilder()
                                .SetMethod(new HttpMethod("PATCH"))
                                .SetRequestUri(requestUri)
                                .SetContent(new PatchContent(value))
                                .SetBearerToken(bearerToken);

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> Delete(string requestUri)
            => await Delete(requestUri, "");

        public static async Task<HttpResponseMessage> Delete(
            string requestUri, string bearerToken)
        {
            var builder = new HttpRequestBuilder()
                                .SetMethod(HttpMethod.Delete)
                                .SetRequestUri(requestUri)
                                .SetBearerToken(bearerToken);

            return await builder.SendAsync();
        }

        public static async Task<HttpResponseMessage> PostFile(string requestUri,
            string filePath, string apiParamName)
            => await PostFile(requestUri, filePath, apiParamName, "");

        public static async Task<HttpResponseMessage> PostFile(string requestUri,
            string filePath, string apiParamName, string bearerToken)
        {
            var builder = new HttpRequestBuilder()
                                .SetMethod(HttpMethod.Post)
                                .SetRequestUri(requestUri)
                                .SetContent(new FileContent(filePath, apiParamName))
                                .SetBearerToken(bearerToken);

            return await builder.SendAsync();
        }
    }
}
