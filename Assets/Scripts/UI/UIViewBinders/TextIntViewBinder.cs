using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace HDV
{
    [RequireComponent(typeof(TMPro.TMP_Text))]
    public class TextIntViewBinder : UIViewBinder<TMPro.TMP_Text>
    {
        protected override void Bind(UIViewModel viewModel)
        {
            var d = Delegate.CreateDelegate(typeof(Action<int>), this, "OnChangeValue");
            var e = (EventInt)viewModel.BindProperty(bindPropertyName, d);
            bindView.text = e.Value.ToString();
        }

        private void OnChangeValue(int value)
        {
            bindView.text = value.ToString();
        }

    }
}
