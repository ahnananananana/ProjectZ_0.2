using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HDV
{
    public class SyncTransform : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private bool syncPosition;
        [SerializeField] private bool syncPosX, syncPosY, syncPosZ;
        [SerializeField] private bool syncRotation;
        [SerializeField] private bool syncRotX, syncRotY, syncRotZ;
        [SerializeField] private Vector3 posOffset, rotOffset;

        [SerializeField] private UnityEvent syncStartEvent, syncEndEvent;

        public Transform Target
        {
            get => target;
            set
            {
                target = value;
                if (value != null)
                    StartCoroutine(StartSync());
            }
        }

        private void Awake()
        {
            StartCoroutine(StartSync());
        }

        private IEnumerator StartSync()
        {
            syncStartEvent?.Invoke();
            while (target != null)
            {
                if(syncPosition)
                {
                    Vector3 syncedPos = transform.position;
                    if (syncPosX)
                        syncedPos.x = target.transform.position.x + posOffset.x;
                    if (syncPosY)
                        syncedPos.y = target.transform.position.y + posOffset.y;
                    if (syncPosZ)
                        syncedPos.z = target.transform.position.z + posOffset.z;
                    transform.position = syncedPos;
                }
                if(syncRotation)
                {
                    Vector3 syncedRot = transform.eulerAngles;
                    if (syncRotX)
                        syncedRot.x = target.transform.eulerAngles.x + rotOffset.x;
                    if (syncRotY)
                        syncedRot.y = target.transform.eulerAngles.y + rotOffset.y;
                    if (syncRotZ)
                        syncedRot.z = target.transform.eulerAngles.z + rotOffset.z;
                    transform.eulerAngles = syncedRot;
                }
                yield return null;
            }
            syncEndEvent?.Invoke();
        }
    }
}
