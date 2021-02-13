using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HDV
{
    public class HealthAlert : MonoBehaviour
    {
        private Image image;
        private Animator animator;
        private IEnumerator healthLowCoroutine;
        private bool isLow, isVeryLow;
        [SerializeField] private float lowMinAlpha, lowMaxAlpha, lowInterval;
        [SerializeField] private float veryLowMinAlpha, veryLowMaxAlpha, veryLowInterval;

        private void Awake()
        {
            image = GetComponent<Image>();
            animator = GetComponent<Animator>();
        }

        public void OnHealthChanged(Stat stat)
        {
            StatCurrent health = (StatCurrent)stat; 
            if ((health.CurrentValue / health.Value) < .5f)
            {
                animator.SetBool("isLow", true);
            }
            else
            {
                animator.SetBool("isLow", false);
                if (image.color.a != 0f)
                    image.color = new Color(1, 0, 0, 0);
            }
            /*if ((health.CurrentValue / health.Value) < .25f)
            {
                isLow = false;
                isVeryLow = true;
                if (healthLowCoroutine == null)
                {
                    healthLowCoroutine = HealthLowEffect();
                    StartCoroutine(healthLowCoroutine);
                }
            }
            else if ((health.CurrentValue / health.Value) < .5f)
            {
                isLow = true;
                isVeryLow = false;
                if (healthLowCoroutine == null)
                {
                    healthLowCoroutine = HealthLowEffect();
                    StartCoroutine(healthLowCoroutine);
                }
            }
            else
            {
                isLow = false;
                isVeryLow = false;
            }*/
        }

        private bool increasingAlpha;

        private IEnumerator HealthLowEffect()
        {
            increasingAlpha = false;
            while (isLow || isVeryLow)
            {
                var color = image.color;
                float targetAlpha;
                float speed;
                if (isLow)
                {
                    speed = lowInterval;
                    if (color.a < lowMaxAlpha && !increasingAlpha)
                    {
                        increasingAlpha = true;
                        targetAlpha = lowMaxAlpha;
                    }
                    else if(color.a <= lowMinAlpha)
                    {
                        increasingAlpha = true;
                        targetAlpha = lowMaxAlpha;
                    }
                    else
                    {
                        increasingAlpha = false;
                        targetAlpha = lowMinAlpha;
                    }
                }
                else
                {
                    speed = veryLowInterval;
                    if (color.a < veryLowMaxAlpha && !increasingAlpha)
                    {
                        increasingAlpha = true;
                        targetAlpha = veryLowMaxAlpha;
                    }
                    else if (color.a <= veryLowMinAlpha)
                    {
                        increasingAlpha = true;
                        targetAlpha = veryLowMaxAlpha;
                    }
                    else
                    {
                        increasingAlpha = false;
                        targetAlpha = veryLowMinAlpha;
                    }
                }

                color.a = Mathf.Lerp(color.a, targetAlpha, Time.deltaTime * speed);
                image.color = color;
                yield return null;
            }

            while(!isLow && isVeryLow && image.color.a > 0f)
            {
                var color = image.color;
                color.a = Mathf.Lerp(color.a, 0f, Time.deltaTime * lowInterval);
                image.color = color;
                yield return null;
            }

            image.color = new Color(1, 0, 0, 0);

            healthLowCoroutine = null;
        }

        /*private IEnumerator DamagedEffect()
        {

        }*/

    }
}
