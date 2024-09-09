using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace Utils
{
    /*
     * Custom Parsers are used to Serialize properties which cannot be directly parsed because of cyclic dependencies.
     */
    public interface ICustomParser
    {
        string Serialize(object value);
        object Deserialize(string value);
    }

    public class Color3Parser : ICustomParser
    {
        public object Deserialize(string value)
        {
            return JsonConvert.DeserializeObject<Color>(value.ToString(), new Color3Convertor());
        }

        public string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value, new Color3Convertor());
        }
    }

    public class Vector3Parser : ICustomParser
    {
        public object Deserialize(string value)
        {
            return JsonConvert.DeserializeObject<Vector3>(value.ToString(), new Vector3Converter());
        }

        public string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value, new Vector3Converter());
        }
    }
    public class Vector2Parser : ICustomParser
    {
        public object Deserialize(string value)
        {
            return JsonConvert.DeserializeObject<Vector2>(value.ToString(), new Vector2Converter());
        }

        public string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value, new Vector2Converter());
        }
    }
    public class TextureParser : ICustomParser
    {
        public object Deserialize(string value)
        {
            return value;
        }

        public string Serialize(object value)
        {
            if (value == null) return string.Empty;
            return ((Texture2D)value).name;
        }
    }
}
