using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class UIAnimatorSequencer : MonoBehaviour
    {
        [SerializeField] private float openInterval, closeInterval;
        [SerializeField] private List<IUIAnimator> animators = new List<IUIAnimator>();

        public void PlayOpen()
        {
            StartCoroutine(Play(openInterval, true));
        }

        public void PlayClose()
        {
            StartCoroutine(Play(closeInterval, false));
        }

        private IEnumerator Play(float interval, bool isOpen)
        {
            if(isOpen)
            {
                for (int i = 0; i < animators.Count; ++i)
                {
                    animators[i].UIAnimator.PlayOpen();
                    if (interval <= 0)
                        continue;
                    else
                        yield return new WaitForSeconds(interval);
                }
            }
            else
            {
                for (int i = 0; i < animators.Count; ++i)
                {
                    animators[i].UIAnimator.PlayClose();
                    if (interval <= 0)
                        continue;
                    else
                        yield return new WaitForSeconds(interval);
                }
            }
        }

        public void AddAnimator(IUIAnimator iAnim)
        {
            animators.Add(iAnim);
        }

        public void RemoveAnimator(IUIAnimator iAnim)
        {
            animators.Remove(iAnim);
        }

    }
}
