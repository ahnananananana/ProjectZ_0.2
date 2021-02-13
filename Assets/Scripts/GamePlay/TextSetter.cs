using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HDV
{
    [RequireComponent(typeof(TMPro.TMP_Text))]
    public class TextSetter : MonoBehaviour
    {
        private TMPro.TMP_Text text;

        private void Awake()
        {
            text = GetComponent<TMPro.TMP_Text>();
        }

        public void SetFloat(float value) => text.text = value.ToString();
        public void SetString(string value) => text.text = value;
    }
}
