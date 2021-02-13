using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [RequireComponent(typeof(TMPro.TMP_Text))]
    public class TextStringViewBinder : UIViewBinder<TMPro.TMP_Text>
    {
        protected override void Bind(UIViewModel viewModel)
        {
            var d = Delegate.CreateDelegate(typeof(Action<string>), this, "OnChangeValue");
            var e = (EventString)viewModel.BindProperty(bindPropertyName, d);
            bindView.text = e?.Value.ToString();
        }

        private void OnChangeValue(string value)
        {
            bindView.text = value.ToString();
        }

    }
}
