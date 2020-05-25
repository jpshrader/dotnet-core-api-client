using dotnet_core_api_client.Client;
using System.Threading.Tasks;
using Xunit;

namespace dotnet_core_api_client.Examples {
	public class ApiReuquestExamples {
		private readonly ApiClient subject;

		public ApiReuquestExamples() {
			subject = new ApiClient();
		}

		[Fact]
		public async Task HitEndpoint() {
			var result = await subject.MakeApiRequest("http://www.google.com");

			Assert.True(result.IsSuccessCode());
		}
	}
}
