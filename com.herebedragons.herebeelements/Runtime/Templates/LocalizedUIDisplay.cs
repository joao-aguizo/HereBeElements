﻿using System;
using com.herebedragons.herebeelements.Runtime.Locale;

namespace com.herebedragons.herebeelements.Runtime.Templates
{
    [Serializable]
    public class LocalizedUIDisplay<T> : UIDisplay where T : Enum
    {
        public bool isLocalized;
        public T[] textKeys;

        protected LocalizedUIDisplay()
        {
            Locale<T>.ChangeLanguageEvent += Clear;
        }

        public override void Show(string text = null)
        {
            if (isLocalized)
                text = Locale<T>.GetText(textKeys[0]);
            base.Show(text);
        }

        public override void Show(long value)
        {
            string text = value.ToString();
            if (isLocalized)
                text = Locale<T>.FormatNumber(value);
            base.Show(text);
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            Show();
        }
#endif
    }
}