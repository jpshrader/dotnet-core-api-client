using dotnet_core_api_client.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_core_api_client.Client {
	public interface IApiClient {
		Task<ApiResponse> MakeApiRequest(
			Uri requestUri,
			RequestMethod requestMethod = RequestMethod.Get,
			IEnumerable<(string key, string value)> headers = null,
			ContentType contentType = ContentType.Json,
			object body = null);
	}

	public class ApiClient : IApiClient {
		private static readonly IEnumerable<(string key, string value)> emptyParams = Enumerable.Empty<(string key, string value)>();

		public async Task<ApiResponse> MakeApiRequest(
			Uri requestUri,
			RequestMethod requestMethod = RequestMethod.Get,
			IEnumerable<(string key, string value)> headers = null,
			ContentType contentType = ContentType.Json,
			object body = null) {
			var cookieContainer = new CookieContainer();
			using var clientHandler = new HttpClientHandler {
				PreAuthenticate = true,
				CookieContainer = cookieContainer
			};
			using var httpClient = new HttpClient(clientHandler);

			var httpRequestMessage = GetHttpRequestMessage(requestUri, requestMethod, contentType, headers, body);

			return await ApiResponse.GetApiResponse(await httpClient.SendAsync(httpRequestMessage), cookieContainer.GetCookies(requestUri));
		}

		private static HttpRequestMessage GetHttpRequestMessage(Uri requestUrl, RequestMethod requestMethod, ContentType contentType, IEnumerable<(string key, string value)> headers, object body) {
			var httpRequestMessage = new HttpRequestMessage {
				RequestUri = requestUrl,
				Method = requestMethod.ToHttpMethod(),
				Content = GetHttpContent(contentType, body)
			};

			foreach (var (key, value) in headers ?? emptyParams)
				httpRequestMessage.Headers.Add(key, value);

			return httpRequestMessage;
		}

		private static StringContent GetHttpContent(ContentType contentType, object body) {
			if (body == null)
				return null;

			return new StringContent(contentType.ToHttpContent(body), Encoding.Default, contentType.ToMediaTypeString());
		}
	}
}
