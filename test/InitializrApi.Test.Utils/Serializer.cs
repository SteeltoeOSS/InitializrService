using System.Text.Json;

namespace Steeltoe.InitializrApi.Test.Utils
{
    public static class Serializer
    {
        private static JsonSerializerOptions Options { get; } = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        public static T DeserializeJson<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, Options);
        }
    }
}
