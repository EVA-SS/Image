using System.Text.Json.Serialization.Metadata;

namespace Image
{
    public static class Json
    {
        public static string? ToJson<T>(this object obj)
        {
            if (obj == null) return null;
            return System.Text.Json.JsonSerializer.Serialize(obj, typeof(T), SGC.Default);
        }

        public static T? ToJson<T>(this string? json, JsonTypeInfo<T> jsonTypeInfo)
        {
            if (json != null && !string.IsNullOrEmpty(json))
            {
                try
                {
                    return System.Text.Json.JsonSerializer.Deserialize(json, jsonTypeInfo);
                }
                catch
                {
                }
            }
            return default;
        }
    }
}