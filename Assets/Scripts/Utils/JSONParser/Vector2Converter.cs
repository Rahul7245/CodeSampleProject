using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Vector2Converter : JsonConverter<Vector2>
{
    public override void WriteJson(JsonWriter writer, Vector2 value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("x");
        writer.WriteValue(value.x);
        writer.WritePropertyName("y");
        writer.WriteValue(value.y);
        writer.WriteEndObject();
    }

    public override Vector2 ReadJson(JsonReader reader, System.Type objectType, Vector2 existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JObject jsonObject = JObject.Load(reader);
        var x = jsonObject.GetValue("x").Value<float>();
        var y = jsonObject.GetValue("y").Value<float>();
        return new Vector2(x, y);
    }
}