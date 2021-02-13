using System;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu]
    public class SettingData : ScriptableObject
    {
        private void OnEnable()
        {
            bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 1f);
            fxVolume = PlayerPrefs.GetFloat("FXVolume", 1f);
            isVibration = PlayerPrefs.GetInt("IsVibration", 1) == 1 ? true : false;
            isAlarm = PlayerPrefs.GetInt("IsAlarm", 1) == 1 ? true : false;
        }

        [SerializeField] private float bgmVolume;
        public float BGMVolume
        {
            get => bgmVolume;
            set
            {
                if (bgmVolume == value)
                    return;
                bgmVolume = value;
                PlayerPrefs.SetFloat("BGMVolume", value);
            }
        }
        

        [SerializeField] private float fxVolume;
        public float FXVolume
        {
            get => fxVolume;
            set
            {
                if (fxVolume == value)
                    return;
                fxVolume = value;
                PlayerPrefs.SetFloat("FXVolume", value);
            }
        }


        [SerializeField] private bool isVibration;
        public bool IsVibration
        {
            get => isVibration;
            set
            {
                if (isVibration == value)
                    return;
                isVibration = value;
                PlayerPrefs.SetInt("IsVibration", value == true ? 1 : 0);
            }
        }


        [SerializeField] private bool isAlarm;
        public bool IsAlarm
        {
            get => isAlarm;
            set
            {
                if (isAlarm == value)
                    return;
                isAlarm = value;
                PlayerPrefs.SetInt("IsAlarm", value == true ? 1 : 0);
            }
        }

    }
}
