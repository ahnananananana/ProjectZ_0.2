using UnityEngine;

namespace HDV
{
    public class ProgressText : MonoBehaviour
    {
        [SerializeField] TMPro.TMP_Text progressText;

        private void Awake()
        {
            progressText.text = "0%";
        }

        public void SetProgressText(float progress)
        {
            progressText.text = ((int)MathUtil.Remap(progress, 0f, 100f)).ToString() + '%'.ToString();
        }
    }
}
