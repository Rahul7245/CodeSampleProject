using System.Collections;
using System.Collections.Generic;
using Utils;
using UnityEngine;
using ElementsListType = System.Collections.Generic.Dictionary<string, (System.Type, object)>;
using System;
/*
 * This class sets all the properties of a material which were saved. 
 * Properties of the material varies for different shaders. 
 * Using C# Reflection we can get and set properties through this class
 */
public class CustomMaterial
{
    public const string DefaultShader = "Universal Render Pipeline/CustomLit";
    public const string EMISSION_COLOR = "_EmissionColor";
    public const string EMISSION_TOGGLE = "_EMISSION";
    public const string SHADER = "shader";
    public const string TILING = "tiling";
    public const string OFFSET = "offset";
    public const string RENDER_QUEUE = "render_queue";
    private Renderer _renderer;
    private MaterialPropertyHandler m_propertyHandler;

    public void Init(Renderer ren, MaterialPropertyHandler propHandler)
    {
        _renderer = ren;
        m_propertyHandler = propHandler;
    }

    IEnumerator LoadMaterialData(ElementsListType[] dict)
    {
        int i = 0;
        yield return new WaitUntil(() => _renderer != null);
        foreach (var element in dict)
        {
            var material = _renderer.materials[i];
            foreach (var (name, (type, value)) in element)
            {
                if (name == SHADER)
                {
                    material.shader = GetShader(value.ToString());
                }
                else if (name == TILING)
                {
                    material.mainTextureScale = (Vector2)value.DeserializeWithCustomConverter(typeof(Vector2));
                }
                else if (name == OFFSET)
                {
                    material.mainTextureOffset = (Vector2)value.DeserializeWithCustomConverter(typeof(Vector2));
                }
                else if (name == RENDER_QUEUE)
                {
                    material.renderQueue = int.Parse(value.ToString());
                }
                else if (name == EMISSION_TOGGLE)
                {
                    if ((bool)value)
                        material.EnableKeyword(EMISSION_TOGGLE);
                    else
                    {
                        material.DisableKeyword(EMISSION_TOGGLE);
                    }
                }
                else
                {
                    SetValue(this, material, name, type, value.DeserializeWithCustomConverter(type), -1);
                }
            }
            i++;
        }
    }

    public Shader GetShader(string shaderName)
    {
        Shader temp = string.IsNullOrWhiteSpace(shaderName) ? Shader.Find(DefaultShader) : Shader.Find(shaderName);
        return temp;
    }

    public void SetValue(CustomMaterial cm, Material mat, string name, Type type, object value, int index)
    {
        cm.m_propertyHandler.SetValue(mat, name, type, value);
    }
}
