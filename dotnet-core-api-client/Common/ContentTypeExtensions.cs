using System;
using System.Text.Json;

namespace dotnet_core_api_client.Common {
	public static class ContentTypeExtensions {
		public static string ToHttpContent(this ContentType contentType, object body) {
			switch (contentType) {
				case ContentType.Json:
					return JsonSerializer.Serialize(body);

				case ContentType.Xml:
					return string.Empty;

				default:
					throw new ArgumentOutOfRangeException($"Given {nameof(ContentType)} was not found");
			};
		}

		public static string ToMediaTypeString(this ContentType contentType) {
			return contentType switch
			{
				ContentType.Json => "application/json",

				ContentType.Xml => "application/xml",

				_ => throw new ArgumentOutOfRangeException($"Given {nameof(ContentType)} was not found"),
			};
			;
		}
	}
}
