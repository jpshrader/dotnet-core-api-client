using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace dotnet_core_api_client.Common {
	public class ApiResponse {
		public static async Task<ApiResponse> GetApiResponse(HttpResponseMessage httpResponseMessage, CookieCollection cookieCollection) {
			return new ApiResponse {
				StatusCode = httpResponseMessage.StatusCode,
				Headers = httpResponseMessage.Headers.ToDictionary(k => k.Key, v => v.Value),
				Cookies = cookieCollection,
				Body = await httpResponseMessage.Content.ReadAsStringAsync()
			};
		}

		public HttpStatusCode StatusCode { get; private set; }

		public IDictionary<string, IEnumerable<string>> Headers { get; private set; }

		public IEnumerable<Cookie> Cookies { get; private set; }

		public string Body { get; private set; }

		public T ToObject<T>() {
			return JsonConvert.DeserializeObject<T>(Body);
		}
	}
}
