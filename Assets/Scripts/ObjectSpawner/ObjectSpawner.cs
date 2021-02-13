using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public abstract class ObjectSpawner<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private T objectPrefab;
        [SerializeField] private Transform targetParent;
        /*[SerializeField] private int poolCount;

        private T[] objectPool;*/
        protected List<T> spawnedObjects = new List<T>();

        protected T Spawn()
        {
            T spawnedObject = Instantiate(objectPrefab);
            if (targetParent != null)
                spawnedObject.transform.SetParent(targetParent);
            spawnedObject.transform.localPosition = Vector3.zero;
            spawnedObject.transform.localRotation = Quaternion.identity;
            spawnedObject.transform.localScale = Vector3.one;
            spawnedObjects.Add(spawnedObject);
            return spawnedObject;
        }
    }
}
