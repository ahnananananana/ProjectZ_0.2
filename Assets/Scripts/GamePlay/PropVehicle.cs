using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HDV
{
    public class PropVehicle : MonoBehaviour, IDestructable, IExplosive, IHittable
    {
        [Header("Hit")]
        [SerializeField] private ParticleSystem hitVFXPrefab;
        [SerializeField] private SubHittablePart[] subHittableParts;
        [SerializeField] private AudioClip hitAudioClip;

        [Header("Explosive")]
        [SerializeField] private bool isBurn;
        [SerializeField] private bool isStartExplosion;
        [SerializeField] private float burnThreshold;
        [SerializeField] private ParticleSystem burnVFXPrefab;
        [SerializeField] private Transform burnPoint;
        [SerializeField] private float explosionRadius, explosionDamage, explosionDelay;
        [SerializeField] private LayerMask explosionMask;
        [SerializeField] private Canvas canvas;
        [SerializeField] private RawImage explosionRadiusFillImage;
        private ParticleSystem burnVFX;
        private bool isVisible;

        [Header("Destruction")]
        [SerializeField] private bool isDestructed;
        [SerializeField] private float hp;
        [SerializeField] private ParticleSystem destructVFXPrefab;
        [SerializeField] private AudioClip explosionAudioClip;

        [SerializeField] private UnityEvent destructEvent, explosionEvent;

        private new Rigidbody rigidbody;
        [SerializeField] private AudioSource explsionAudioSource;

        public float HP { get => hp; set => hp = value; }

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();

            for (int i = 0; i < subHittableParts.Length; ++i)
                subHittableParts[i].MainHittable = this;
        }

        public void Destruct()
        {
            rigidbody.isKinematic = false;
            for (int i = 0; i < subHittableParts.Length; ++i)
                subHittableParts[i].Rigidbody.isKinematic = false;
            destructEvent?.Invoke();

            isDestructed = true;
        }

        public float TakeDamage(DamageData damageData, ContactData contactData)
        {
            Instantiate(hitVFXPrefab, contactData.Point, Quaternion.LookRotation(contactData.Normal));

            var source = damageData.Source as IAudioSource;
            source.AudioSource.clip = hitAudioClip;
            source.AudioSource.Play(); 

             hp -= damageData.Damage;
            if(!isBurn && hp < burnThreshold)
            {
                Burn();
            }

            if (!isStartExplosion && hp <= 0)
            {
                StartCoroutine(StartExplode());
            }

            return damageData.Damage;
        }

        public void Explode()
        {
            Instantiate(destructVFXPrefab, transform.position, transform.rotation);
            if (burnVFX != null)
                burnVFX.Stop();

            var colliders = Physics.OverlapSphere(transform.position, explosionRadius, explosionMask);
            if(colliders != null)
            {
                for(int i = 0; i < colliders.Length; ++i)
                {
                    if(colliders[i].TryGetComponent<IHittable>(out var hittable))
                    {
                        hittable.TakeDamage(new DamageData(this, explosionDamage), default);
                    }
                }
            }

            if(isVisible)
            {
                explosionEvent?.Invoke();
                explsionAudioSource.clip = explosionAudioClip;
                explsionAudioSource.Play();
            }
                
        }

        private void OnBecameVisible()
        {
            isVisible = true;
        }

        private void OnBecameInvisible()
        {
            isVisible = false;
        }

        private IEnumerator StartExplode()
        {
            isStartExplosion = true;
            canvas.enabled = true;

            float scaleSpeed = explosionDelay > 0 ? Vector3.Distance(explosionRadiusFillImage.transform.localScale, Vector3.one) / explosionDelay : 1f;
            while(Vector3.Distance(explosionRadiusFillImage.transform.localScale, Vector3.one) > .001f)
            {
                explosionRadiusFillImage.transform.localScale = Vector3.MoveTowards(explosionRadiusFillImage.transform.localScale, Vector3.one, Time.deltaTime * scaleSpeed);
                yield return null;
            }
            explosionRadiusFillImage.transform.localScale = Vector3.one;
            yield return null;
            Explode();
            Destruct();
            canvas.enabled = false;
        }

        private void Burn()
        {
            burnVFX = Instantiate(burnVFXPrefab, burnPoint.position, burnPoint.rotation);
            isBurn = true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
    }

}