using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class FootSound : MonoBehaviour
    {
        //[SerializeField] private AudioClip audioClip;
        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void OnFootStep()
        {
            //audioSource.Play();
        }
    }
}
