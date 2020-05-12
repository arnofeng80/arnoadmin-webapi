using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.Utils
{
    class JsonHelper
    {
    }

    public class StringJsonConverter : JsonConverter
    {
        public StringJsonConverter() { }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return long.Parse(reader.Value.ToString());
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            string sValue = value.ToString();
            writer.WriteValue(sValue);
        }
    }
}
