using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class UIMultiToggleGroup : MonoBehaviour
    {
        private List<IToggle> toggles = new List<IToggle>();
        private IToggle currentToggled;

        [SerializeField] private IToggleEvent setToggleEvent, unSetToggleEvent;

        public void Reset()
        {
            if(currentToggled != null)
            {
                currentToggled.SetToggle(false);
                currentToggled = null;
            }
        }

        public void SetToggle(IToggle toggle)
        {
            if(toggles.Contains(toggle))
            {
                if (currentToggled != null)
                {
                    currentToggled.SetToggle(false);
                    unSetToggleEvent?.Invoke(currentToggled);
                }
                currentToggled = toggle;
                toggle.SetToggle(true);
                setToggleEvent?.Invoke(toggle);
            }
        }

        public void AddToggle(IToggle toggle)
        {
            toggles.Add(toggle);
            toggle.SetToggle(false);
        }

        public void RemoveToggle(IToggle toggle)
        {
            if(toggles.Remove(toggle) && currentToggled == toggle)
            {
                toggle.SetToggle(false);
                currentToggled = null;
            }
        }
    }
}

