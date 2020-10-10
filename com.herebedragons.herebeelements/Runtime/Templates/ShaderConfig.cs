﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

namespace com.herebedragons.herebeelements.Runtime.Templates
{
    [Serializable]
    public class ShaderConfig
    {
        [Header("General")] 
        [Range(0, 1)]
        [Tooltip("Sets the overall opacity")]
        public float opacity = 1;
        
        [ColorUsage(true, true)]
        [Tooltip("Sets the color tint")]
        public Color tintColor = Color.white;
        [Range(0,1)]
        [Tooltip("Determines the 0 level opacity")]
        public float alphaClipThreshold = 0;
        [Tooltip("Turns Border On or Off")]
        public bool border = false;

        [Range(0, 0.99f)] public float borderWidth = 0.5f;
        [ColorUsage(true, true)]
        public Color borderColor = Color.white;

        private readonly Dictionary<int, Object> _cachedValues = new Dictionary<int, object>();

        // Base
        internal static readonly int Opacity = Shader.PropertyToID("opacity");
        internal static readonly int AlphaClip = Shader.PropertyToID("alphaClip");
        internal static readonly int TintColor = Shader.PropertyToID("_Color");

        // Border
        internal static readonly int Border = Shader.PropertyToID("border");
        internal static readonly int BorderWidth = Shader.PropertyToID("borderWidth");
        internal static readonly int BorderColor = Shader.PropertyToID("borderColor");

        private Material _material;

        internal void ApplyConfig(Renderer renderer)
        {
            if (renderer == null) return;
            renderer.material = ApplyConfig(renderer.material);
        }

        internal void ApplyConfig(Image image)
        {
            if (image == null) return;
            image.material = ApplyConfig(image.material);
        }

        internal void ApplyConfig(Graphic graphic)
        {
            if (graphic == null) return;
            graphic.material = ApplyConfig(graphic.material);
        }

        private Material ApplyConfig(Material material)
        {
            _material = new Material(material);
            // general
            SetFloat(Opacity, opacity);
            SetFloat(AlphaClip, alphaClipThreshold);
            SetColor(TintColor, tintColor);
            
            // Border
            SetBool(Border, border);
            SetFloat(BorderWidth, borderWidth);
            SetColor(BorderColor, borderColor);
            
            return _material;
        }

        private void CacheValues()
        {
            CacheValues(_material);
        }

        private void CacheValues(Material material)
        {
            // Base
            _cachedValues[Opacity] = material.GetFloat(Opacity);
            _cachedValues[AlphaClip] = material.GetFloat(AlphaClip);
            _cachedValues[TintColor] = material.GetColor(TintColor);
            // Border
            _cachedValues[Border] = material.GetInt(Border) == 1;
            _cachedValues[BorderColor] = material.GetColor(BorderColor);
            _cachedValues[BorderWidth] = material.GetFloat(BorderWidth);
        }

        public void SetBool(int key, bool value)
        {
            if (CheckMaterial(key, value))
            {
                _material.SetInt(key, value ? 1 : 0);
                _cachedValues[key] = value;
            }
        }

        public void SetColor(int key, Color color)
        {
            if (CheckMaterial(key, color))
            {
                _material.SetColor(key, color);
                _cachedValues[key] = color;
            }
        }

        public void SetFloat(int key, float value)
        {
            if (CheckMaterial(key, value))
            {
                _material.SetFloat(key, value);
                _cachedValues[key] = value;
            }
        }

        private bool CheckMaterial(int key, Object value)
        {
            if (_material == null)
                return false;
            if (_cachedValues.TryGetValue(key, out Object o))
                return !o.Equals(value);
            return true;
        }
    }
}