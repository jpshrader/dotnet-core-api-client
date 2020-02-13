using dotnet_core_api_client.Common;
using System;
using System.Collections.Generic;
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
}
