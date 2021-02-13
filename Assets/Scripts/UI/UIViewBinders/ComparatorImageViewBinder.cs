using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HDV
{
    [RequireComponent(typeof(Image))]
    public class ComparatorImageViewBinder : UIViewBinder<Image>
    {
        [SerializeField] private Sprite equalImage, greatImage, lessImage;

        protected override void Bind(UIViewModel viewModel)
        {
            var d = Delegate.CreateDelegate(typeof(Action<Comparator>), this, "OnChangeValue");
            var e = viewModel.BindProperty(bindPropertyName, d) as EventValueType<Comparator>;
            bindView.preserveAspect = true;
            bindView.sprite = GetImage(e.Value);
        }

        private void OnChangeValue(Comparator changedImage)
        {
            bindView.sprite = GetImage(changedImage);
        }

        private Sprite GetImage(Comparator comparator)
        {
            switch(comparator)
            {
                case Comparator.Equals:
                    return equalImage;
                case Comparator.GreaterThan:
                    return greatImage;
                default:
                    return lessImage;
            }
        }

    }
}
