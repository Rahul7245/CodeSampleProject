using System;
using UnityEngine;

namespace Utils
{
    public interface IValueGetterSetter

    {
        object GetValue(Material mat, string name);
        void SetValue(Material mat, string name, object value);
    }

    public class TextureGetter : IValueGetterSetter
    {
        public object GetValue(Material mat, string name)
        {
            return mat.GetTexture(name);
        }

        public void SetValue(Material mat, string name, object value)
        {

            OnTextureValueChanged(mat, name, value);
        }


        public void OnTextureValueChanged(Material mat, string name, object value)
        {
            /*
             * TODO: Write your own value setter here
             */

            /* if (value != null)
             {
                 if (string.IsNullOrEmpty(value.ToString())) {
                     mat.SetTexture(name, null);
                     return;
                 }
                 if (value.GetType().ToString() == "UnityEngine.Texture2D")
                 {
                     mat.SetTexture(name, (Texture)value);
                     return;
                 }
                 string textureUrl;
                 if (value.GetType() == typeof(string))
                 {
                     textureUrl = (string)value;
                 }
                 else
                 {
                     var data = (TextureAPI.TextureData)value;
                     textureUrl = data?.texture;
                 }

                 if (!string.IsNullOrEmpty(textureUrl) && textureUrl.Contains("http"))
                 {
                     var textureTracker = SystemOp.Resolve<TextureTracker>();
                     if (Helper.DownloadLowResTextures())
                         textureUrl = Helper.GetDownScaledURL(textureUrl);
                     if (textureTracker.ContainsURL(textureUrl))
                     {
                         textureTracker.AddToPendingCallback(textureUrl, (texture)=> mat.SetTexture(name, texture));
                         return;
                     }
                     textureTracker.AddTextureToList(textureUrl, null);
                     new GetTexture(textureUrl).DoRequest((status, texture) =>
                     {
                         if (status)
                         {
                             textureTracker.AddTextureToList(textureUrl, texture);
                             mat.SetTexture(name, texture);
                         }
                     });
                 }
             }
             else
             {
                 //var textureUrl = string.Empty;
                 // isTextureRemoved = true;
                  mat.SetTexture(name, null);
                 // OnTextureReceived(true, null);
             }*/

        }

        public void OnTextureReceived(bool status, Texture texture)
        {
            if (!status)
            {
                return;
            }

        }

    }

    public class ColorGetter : IValueGetterSetter
    {
        public object GetValue(Material mat, string name)
        {
            return mat.GetColor(name);
        }

        public void SetValue(Material mat, string name, object value)
        {
            mat.SetColor(name, (Color)value);
        }
    }

    public class FloatGetter : IValueGetterSetter
    {
        public object GetValue(Material mat, string name)
        {
            return mat.GetFloat(name);
        }

        public void SetValue(Material mat, string name, object value)
        {
            mat.SetFloat(name, (float)Convert.ToDecimal(value));
        }
    }

    public class BoolGetter : IValueGetterSetter
    {
        public object GetValue(Material mat, string name)
        {
            return Convert.ToBoolean(mat.GetFloat(name));
        }

        public void SetValue(Material mat, string name, object value)
        {
            mat.SetFloat(name, (float)Convert.ToDecimal(value));
        }
    }

    public class Vector3Getter : IValueGetterSetter
    {
        public object GetValue(Material mat, string name)
        {
            Vector3 val = mat.GetVector(name);
            return val;
        }

        public void SetValue(Material mat, string name, object value)
        {
            mat.SetVector(name, (Vector3)value);
        }
    }
}
