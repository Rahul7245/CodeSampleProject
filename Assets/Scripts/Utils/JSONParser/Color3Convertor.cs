using System;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Color3Convertor : JsonConverter<Color>
{
    public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("r");
        writer.WriteValue(value.r);
        writer.WritePropertyName("g");
        writer.WriteValue(value.g);
        writer.WritePropertyName("b");
        writer.WriteValue(value.b);
        writer.WritePropertyName("a");
        writer.WriteValue(value.a);
        writer.WriteEndObject();
    }

    public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        JObject jsonObject = JObject.Load(reader);
        var r = jsonObject.GetValue("r").Value<float>();
        var g = jsonObject.GetValue("g").Value<float>();
        var b = jsonObject.GetValue("b").Value<float>();
        var a = jsonObject.GetValue("a").Value<float>();
        return new Color(r, g, b,a);
    }
}