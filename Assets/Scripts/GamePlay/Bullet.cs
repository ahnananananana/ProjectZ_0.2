using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class Bullet : MonoBehaviour, IAudioSource
    {
        [SerializeField] private LayerMask targetLayerMask;
        private object source;
        private float speed;
        private AudioSource audioSource;
        [SerializeField] private MeshRenderer meshRenderer;

        public AudioSource AudioSource => audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.pitch = Random.Range(.9f, 1.1f);
            meshRenderer.enabled = false;
        }

        public void Init(object source, float speed, LayerMask targetLayerMask)
        {
            this.source = source;
            this.speed = speed;
            this.targetLayerMask.value |= targetLayerMask.value;
        }

        public void Depart()
        {
            meshRenderer.enabled = true;
            StartCoroutine(Moving());
        }

        private IEnumerator Moving()
        {
            while(true)
            {
                Ray ray = new Ray(transform.position, transform.forward);
                if (Physics.Raycast(ray, out RaycastHit hit, speed * Time.fixedDeltaTime, targetLayerMask))
                {
                    ContactData contactData = new ContactData(hit.point, hit.normal, hit.collider);
                    if(source is IAttackable)
                    {
                        var attackable = source as IAttackable;
                        attackable.OnAttackHit(this, contactData);
                        meshRenderer.enabled = false;
                        yield return new WaitUntil(() => !audioSource.isPlaying);
                    }
                    break;
                }
                transform.position += transform.forward * speed * Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
            BulletObjectPool.current.ReturnObject(this);
        }
    }
}
