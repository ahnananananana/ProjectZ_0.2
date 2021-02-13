using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class MultiToggleGroup : MonoBehaviour
    {
        private List<MultiToggle> toggles = new List<MultiToggle>();
        public void RegisterToggle(MultiToggle toggle)
        {
            toggles.Add(toggle);
        }

        public void Toggled(MultiToggle toggle)
        {
            for(int i = 0; i < toggles.Count; ++i)
            {
                if(toggle != toggles[i])
                {
                    toggles[i].Disable();
                }
            }
        }

    }
}
