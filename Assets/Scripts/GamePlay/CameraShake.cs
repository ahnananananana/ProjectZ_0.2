using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace HDV
{
    public class CameraShake : MonoBehaviour
    {
        private CinemachineBasicMultiChannelPerlin cinemachinePerlin;

        [SerializeField] private float gunShotAmp, gunShotFrq, gunShotTime;
        [SerializeField] private float carExplosionAmp, carExplosionFrq, carExplosionTime;
        private IEnumerator gunShotCoroutine;


        private void Awake()
        {
            cinemachinePerlin = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            
        }

        public void StartShakeByGunShot()
        {
            if(gunShotCoroutine == null)
            {
                gunShotCoroutine = ShakeGunShot();
                StartCoroutine(gunShotCoroutine);
            }
        }

        private IEnumerator ShakeGunShot()
        {
            cinemachinePerlin.m_AmplitudeGain = gunShotAmp;
            cinemachinePerlin.m_FrequencyGain = gunShotFrq;
            yield return new WaitForSeconds(gunShotTime);
            cinemachinePerlin.m_AmplitudeGain = 0f;
            cinemachinePerlin.m_FrequencyGain = 0f;
            gunShotCoroutine = null;
        }

        public void StartShakeByCarExplosion()
        {
            StartCoroutine(ShakeCarExplosion());
        }

        private IEnumerator ShakeCarExplosion()
        {
            cinemachinePerlin.m_AmplitudeGain = carExplosionAmp;
            cinemachinePerlin.m_FrequencyGain = carExplosionFrq;
            yield return new WaitForSeconds(carExplosionTime);
            cinemachinePerlin.m_AmplitudeGain = 0f;
            cinemachinePerlin.m_FrequencyGain = 0f;
        }
    }
}

