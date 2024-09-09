using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Utils
{
    public class MaterialPropertyHandler
    {
        private readonly Dictionary<Type, IValueGetterSetter> _valueGetters;

        public MaterialPropertyHandler()
        {
            _valueGetters = new Dictionary<Type, IValueGetterSetter>
        {
            { typeof(Texture), new TextureGetter() },
            { typeof(Color), new ColorGetter() },
            { typeof(float), new FloatGetter() },
            { typeof(bool), new BoolGetter() },
            { typeof(Vector3), new Vector3Getter() },
            { typeof(Range), new FloatGetter() },
        };
        }

        public object GetValue(Material mat, string name, Type type)
        {
            if (_valueGetters.TryGetValue(type, out var getter))
            {
                return getter.GetValue(mat, name);
            }
            return null;
        }

        public void SetValue(Material mat, string name, Type type, object value)
        {
            if (_valueGetters.TryGetValue(type, out var setter))
            {
                 setter.SetValue(mat, name,value);
            }
        }
    }

    public static class SerializationExtensions
    {
        public static object SerializeWithCustomConverter(this object value, Type type)
        {
            if (type == typeof(Color))
            {
                var converter = new Color3Parser();
                return converter.Serialize(value);
            }
            else if (type == typeof(Vector3))
            {
                var converter = new Vector3Parser();
                return converter.Serialize(value);
            }
            else if (type == typeof(Vector2))
            {
                var converter = new Vector2Parser();
                return converter.Serialize(value);
            }
            else if (type == typeof(Texture))
            {
                var converter = new TextureParser();
                return converter.Serialize(value);
            }
            else
            {
                return value; // Return the original value for other types
            }
        }
        public static object DeserializeWithCustomConverter(this object value, Type type)
        {
            if (type == typeof(Color))
            {
                var converter = new Color3Parser();
                return converter.Deserialize(value.ToString());
            }
            else if (type == typeof(Vector3))
            {
                var converter = new Vector3Parser();
                return converter.Deserialize(value.ToString());
            }
            else if (type == typeof(Vector2))
            {
                var converter = new Vector2Parser();
                return converter.Deserialize(value.ToString());
            }
            else
            {
                return value; // Return the original value for other types
            }
        }

        public static Type ToType(this ShaderPropertyType propertyType, string[] attribute = null)
        {
            switch (propertyType)
            {
                case ShaderPropertyType.Color:
                    return typeof(Color);
                case ShaderPropertyType.Vector:
                    return typeof(Vector3);
                case ShaderPropertyType.Range:
                    return typeof(Range); // Assuming Range is represented by float
                case ShaderPropertyType.Float:
                    if (attribute != null && attribute.Length > 0 && attribute[0].Contains("Toggle"))
                        return typeof(bool);
                    return typeof(float);
                case ShaderPropertyType.Texture:
                    return typeof(Texture);
                default:
                    throw new ArgumentOutOfRangeException(nameof(propertyType), propertyType, null);
            }
        }
    }
}
