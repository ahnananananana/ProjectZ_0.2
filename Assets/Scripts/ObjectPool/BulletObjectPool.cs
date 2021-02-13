using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class BulletObjectPool : ObjectPool<BulletObjectPool, Bullet>
    {
       /* private static BulletObjectPool instance;

        public static BulletObjectPool current
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<BulletObjectPool>();
                if (instance == null)
                {
                    var prefab = Resources.Load<BulletObjectPool>("Singletons/BulletObjectPool");
                    instance = Instantiate(prefab);
                }
                return instance;
            }
        }

        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private int poolSize;
        private Queue<Bullet> pool = new Queue<Bullet>();

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }

            for(int i = 0; i < poolSize; ++i)
            {
                var bullet = Instantiate(bulletPrefab);
                bullet.transform.SetParent(transform);
                bullet.gameObject.SetActive(false);
                pool.Enqueue(bullet);
            }
        }

        public Bullet GetBullet()
        {
            if(pool.Count == 0)
                return Instantiate(bulletPrefab);
            else
            {
                var bullet = pool.Dequeue();
                bullet.gameObject.SetActive(true);
                bullet.transform.SetParent(null);
                return bullet;
            }
        }

        public void ReturnBullet(Bullet bullet)
        {
            bullet.transform.SetParent(transform);
            bullet.gameObject.SetActive(false);
            pool.Enqueue(bullet);
        }*/
    }
}
