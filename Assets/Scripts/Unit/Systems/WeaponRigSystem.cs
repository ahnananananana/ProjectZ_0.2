using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace HDV
{
    [Serializable]
    public class WeaponRigData
    {
        public Vector3 WeaponPosePos, WeaponPoseRot, RightHandPos, RightHandRot, RightHintPos, LeftHandPos, LeftHandRot, LeftHintPos;
    }

    public class WeaponRigSystem : RigSystem
    {
        [SerializeField] private Rig aimRig, handRig, weaponPoseRig;
        [SerializeField] private Transform rightHand, leftHand, rightHint, leftHint;
        [SerializeField] private MultiPositionConstraint weaponPosConstraint;
        [SerializeField] private MultiRotationConstraint weaponRotConstraint;

        public void SetWeaponRigData(WeaponRigData data)
        {
            rightHand.localPosition = data.RightHandPos;
            rightHand.localEulerAngles = data.RightHandRot;
            leftHand.localPosition = data.LeftHandPos;
            leftHand.localEulerAngles = data.LeftHandRot;
            rightHint.localPosition = data.RightHintPos;
            leftHint.localPosition = data.LeftHintPos;
            weaponPosConstraint.data.offset = data.WeaponPosePos;
            weaponRotConstraint.data.offset = data.WeaponPoseRot;
        }

        public void SetWeaponPoseRig(float value) => weaponPoseRig.weight = value;
        public void SetHandRig(float value) => handRig.weight = value;
        public void SetAimRig(float value) => aimRig.weight = value;
    }
}
