using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public abstract class ObjectPool<T, O> : MonoBehaviour
        where T : MonoBehaviour
        where O : Component
    {
        private static T instance;

        public static T current
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    var prefab = Resources.Load<T>("Singletons/" + typeof(T).Name);
                    instance = Instantiate(prefab);
                }
                return instance;
            }
        }

        [SerializeField] private O prefab;
        [SerializeField] private int poolSize = 30;
        private Queue<O> pool = new Queue<O>();

        private void Awake()
        {
            if (instance == null)
                instance = this as T;
            else
            {
                Destroy(gameObject);
                return;
            }

            for (int i = 0; i < poolSize; ++i)
            {
                var obj = Instantiate(prefab);
                obj.transform.SetParent(transform);
                obj.gameObject.SetActive(false);
                pool.Enqueue(obj);
            }
        }

        public O GetObject()
        {
            if (pool.Count == 0)
                return Instantiate(prefab);
            else
            {
                var obj = pool.Dequeue();
                obj.gameObject.SetActive(true);
                obj.transform.SetParent(null);
                return obj;
            }
        }

        public void ReturnObject(O obj)
        {
            obj.transform.SetParent(transform);
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }
}
