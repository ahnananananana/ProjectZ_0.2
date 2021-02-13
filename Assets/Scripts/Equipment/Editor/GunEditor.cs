using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Animations.Rigging;

namespace HDV
{
    [CustomEditor(typeof(Gun))]
    public class GunEditor : Editor
    {
        private GunBaseData gunBaseData;
        private Transform rigTF, weaponRefTF;
        private Transform rightHand, rightHint, leftHand, leftHint;
        private MultiPositionConstraint weaponPosePos;
        private MultiRotationConstraint weaponPoseRot;

        private void OnEnable()
        {
            var gun = target as Gun;
            if (gun.transform.parent?.parent?.parent?.parent != null)
            {
                rigTF = gun.transform.parent.parent.parent.parent.Find("Rig");
                weaponRefTF = gun.transform.parent;
                rightHand = weaponRefTF.Find("RightHandRef");
                rightHint = weaponRefTF.Find("RIghtHintRef");
                leftHand = weaponRefTF.Find("LeftHandRef");
                leftHint = weaponRefTF.Find("LeftHintRef");
                var weaponPose = rigTF.Find("WeaponRig").Find("NormalPose");
                weaponPosePos = weaponPose.GetComponent<MultiPositionConstraint>();
                weaponPoseRot = weaponPose.GetComponent<MultiRotationConstraint>();
            }

            gunBaseData = gun.GunData.BaseData;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUI.enabled = Application.isPlaying && rigTF != null && weaponRefTF != null;
            if(GUILayout.Button("Save Rigging Data"))
            {
                gunBaseData.WeaponRigData.RightHandPos = rightHand.localPosition;
                gunBaseData.WeaponRigData.RightHandRot = rightHand.localEulerAngles;
                gunBaseData.WeaponRigData.RightHintPos = rightHint.localPosition;
                gunBaseData.WeaponRigData.LeftHandPos = leftHand.localPosition;
                gunBaseData.WeaponRigData.LeftHandRot = leftHand.localEulerAngles;
                gunBaseData.WeaponRigData.LeftHintPos = leftHint.localPosition;
                gunBaseData.WeaponRigData.WeaponPosePos = weaponPosePos.data.offset;
                gunBaseData.WeaponRigData.WeaponPoseRot = weaponPoseRot.data.offset;
                EditorUtility.SetDirty(gunBaseData);
            }
            GUI.enabled = true;
        }
    }
}
