using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu(menuName = "UIWindowModel/UIWindowModel")]
    public class UIWindowModel : ScriptableObject
    {
        [SerializeField] private UIWindowHistoryManager windowHistoryManager;
        public UIWindow window { get; set; }

        public void Open()
        {
            window.Open();
        }

        public void Close()
        {
            window.Close();
        }
    }
}
