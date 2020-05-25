using dotnet_core_api_client.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_core_api_client.Client {
	public interface IApiClient {
		Task<ApiResponse> MakeApiRequest(
			string requestUrl,
			RequestMethod requestMethod = RequestMethod.Get,
			ContentType contentType = ContentType.Json,
			object body = null,
			IEnumerable<(string key, string value)> queryParams = null,
			IEnumerable<(string key, string value)> headers = null);
	}
}
