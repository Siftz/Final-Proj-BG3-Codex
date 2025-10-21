using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LogBg3Armory.Extensions
{
    public static class SessionExtensions
    {
        // Save object as JSON string in session
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        // Retrieve object from JSON string in session
        public static T? GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}