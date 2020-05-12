using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.Utils
{
    public class LongJsonConverter : JsonConverter<long>
    {
        public override long ReadJson(JsonReader reader, Type objectType, long existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            long val = 0L;
            long.TryParse(reader.Value.ToString(), out val);
            return val;
        }

        public override void WriteJson(JsonWriter writer, long value, JsonSerializer serializerr)
        {
            string sValue = value.ToString();
            writer.WriteValue(sValue);
        }
    }
}
