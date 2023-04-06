using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HangFireTest.Services.Utility
{
    public static class JsonSerializeExtensions
    {
        private static readonly JsonSerializerOptions _options;

        static JsonSerializeExtensions()
        {
            _options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public static string ToJson(this object data)
        {
            return JsonSerializer.Serialize(data, _options);
        }

        public static T FromJson<T>(this string data)
        {
            return JsonSerializer.Deserialize<T>(data, _options);
        }
    }
}
