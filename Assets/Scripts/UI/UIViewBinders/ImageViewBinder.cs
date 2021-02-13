using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HDV
{
    public class ImageViewBinder : UIViewBinder<Image>
    {
        protected override void Bind(UIViewModel viewModel)
        {
            var d = Delegate.CreateDelegate(typeof(Action<Sprite>), this, "OnChangeValue");
            var e = viewModel.BindProperty(bindPropertyName, d) as EventSprite;
            bindView.preserveAspect = true;
            bindView.sprite = e.Value;
        }

        private void OnChangeValue(Sprite changedImage)
        {
            bindView.sprite = changedImage;
        }
    }
}
