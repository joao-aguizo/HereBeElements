﻿using System;
using HereBeElements.Locale;

namespace HereBeElements.UI
{
    [Serializable]
    public class LocalizedUIDisplay<T> : UIDisplay where T : Enum
    {
        public bool isLocalized;
        public T[] textKeys;
        public string separator = ":";

        protected LocalizedUIDisplay()
        {
            //Locale<T>.ChangeLanguageEvent += Clear;
        }

        public override void Show(string text = null)
        {
            if (isLocalized && textKeys.Length > 0)
                text = Locale<T>.GetText(textKeys[0]);
            base.Show(text + separator);
        }

        public override void Show(long value)
        {
            string text = value.ToString();
            if (isLocalized)
                text = Locale<T>.FormatNumber(value);
            base.Show(text);
        }

        protected override void Awake()
        {
            base.Awake();
            Show();
        }
        
        protected override void Subscribe(bool onOff = true)
        {
            base.Subscribe(onOff);
            if (onOff)
            {
                Locale<T>.ChangeLanguageEvent += Clear;
            }
            else
            {
                Locale<T>.ChangeLanguageEvent -= Clear;
            }
        }
    }
}