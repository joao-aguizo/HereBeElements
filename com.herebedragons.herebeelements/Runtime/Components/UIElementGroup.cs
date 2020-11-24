﻿using System;
using HereBeElements.Internal;
using UnityEngine;

namespace HereBeElements
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIElementGroup : UIElement
    {
        private UIElement[] _children;
        private CanvasGroup _cg;
    
        protected override void Awake()
        {
            base.Awake();
            _children = Utils.GetComponentsInChildren<UIElement>(this);
            _cg = GetComponent<CanvasGroup>();
        }

        public override void Show(bool onOff = true)
        {
            _cg.alpha = onOff ? 1 : 0;
        }

        public override void Enable(bool onOff = true)
        {
            _cg.interactable = onOff;
        }


        public override void Highlight(bool onOff = true)
        {
            InvokeAll(el => el.Highlight(onOff));
            base.Highlight(onOff);
        }

        private void InvokeAll(Action<UIElement> action)
        {
            if (_children == null) return;
            foreach (UIElement child in _children)
                action.Invoke(child);
        }
        
    }
}