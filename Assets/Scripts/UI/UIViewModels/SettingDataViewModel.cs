using System;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu(menuName = "ViewModels/SettingDataViewModel")]
    public class SettingDataViewModel// : UIViewModel<SettingData>
    {
        #region Property
        private float bgmVolume;
        public float BGMVolume
        {
            get => bgmVolume;
            set
            {
                if (bgmVolume == value)
                    return;
                bgmVolume = value;
                BgmVolumeChangeEvent?.Invoke(value);
            }
        }
        public event Action<float> BgmVolumeChangeEvent;


        private float fxVolume;
        public float FXVolume
        {
            get => fxVolume;
            set
            {
                if (fxVolume == value)
                    return;
                fxVolume = value;
                FxVolumeChangeEvent?.Invoke(value);
            }
        }
        public event Action<float> FxVolumeChangeEvent;


        private bool isVibration;
        public bool IsVibration
        {
            get => isVibration;
            set
            {
                if (isVibration == value)
                    return;
                isVibration = value;
                VibrationChangeEvent?.Invoke(value);
            }
        }
        public event Action<bool> VibrationChangeEvent;


        private bool isAlarm;
        public bool IsAlarm
        {
            get => isAlarm;
            set
            {
                if (isAlarm == value)
                    return;
                isAlarm = value;
                AlarmChangeEvent?.Invoke(value);
            }
        }
        public event Action<bool> AlarmChangeEvent;
        #endregion

       /* protected override void OnBindModel(SettingData model)
        {
            bgmVolume = model.BGMVolume;
            fxVolume = model.FXVolume;
            isVibration = model.IsVibration;
            isAlarm = model.IsAlarm;

            BgmVolumeChangeEvent += (i) => model.BGMVolume = i;
            FxVolumeChangeEvent += (i) => model.FXVolume = i;
            VibrationChangeEvent += (i) => model.IsVibration = i;
            AlarmChangeEvent += (i) => model.IsAlarm = i;
        }*/

    }
}
