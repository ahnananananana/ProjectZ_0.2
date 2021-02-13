using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [RequireComponent(typeof(ParticleSystem))]
    public class Particle : MonoBehaviour
    {
        private new ParticleSystem particleSystem;

        private void Awake()
        {
            particleSystem = GetComponent<ParticleSystem>();
        }

        private void OnEnable()
        {
            particleSystem.Play();
        }

        private void OnDisable()
        {
            particleSystem.Simulate(0f);
        }

        private void OnParticleSystemStopped()
        {
            ParticleObjectPool.current.ReturnObject(particleSystem);
        }
    }
}
