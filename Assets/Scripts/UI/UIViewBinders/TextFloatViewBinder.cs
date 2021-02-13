using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace HDV
{
    [RequireComponent(typeof(TMPro.TMP_Text))]
    public class TextFloatViewBinder : UIViewBinder<TMPro.TMP_Text>
    {
        protected override void Bind(UIViewModel viewModel)
        {
            var d = Delegate.CreateDelegate(typeof(Action<float>), this, "OnChangeValue");
            var e = (EventFloat)viewModel.BindProperty(bindPropertyName, d);
            bindView.text = e.Value.ToString();
        }

        private void OnChangeValue(float value)
        {
            bindView.text = value.ToString();
        }

    }
}
