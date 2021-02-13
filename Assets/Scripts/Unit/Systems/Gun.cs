using UnityEngine;
using UnityEngine.Events;

namespace HDV
{
    public class Gun : MonoBehaviour, IInteractable
    {
        [SerializeField] private GunData gunData;
        [SerializeField] private GunModel gunModel;
        [SerializeField] private Transform uiInfoTF;
        private new Rigidbody rigidbody;
        private new Collider collider;
        private AudioSource audioSource;

        [SerializeField] private UnityEvent fireEvent, highlightOnEvent, highlightOffEvent;

        public GunData GunData => gunData;
        public GunModel GunModel => gunModel;
        public Vector3 Position => transform.position;
        public Rigidbody Rigidbody => rigidbody; 
        public Collider Collider => collider;
        public Vector3 UIInfoPosition => uiInfoTF.position;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            collider = GetComponent<Collider>();
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = gunData.BaseData.FireAudioClip;

            if (gunData != null && gunData.BaseData != null && !gunData.IsInit)
                Init(gunData);
        }

        public void Init(GunData gunData)
        {
            Debug.Assert(gunData != null || this.gunData != null);
            if (gunData == null)
                gunData = this.gunData;
            else
                this.gunData = gunData;

            if (!gunData.IsInit)
                gunData.Init();

            /*if (gunModel != null)
            {
                Destroy(gunModel.gameObject);
                gunModel = null;
            }*/

            /*if (gunModel == null)
            {
                gunModel = Instantiate(gunData.BaseData.GunModel, transform);
                gunModel.transform.localPosition = Vector3.zero;
                gunModel.transform.localRotation = Quaternion.identity;
                gunModel.transform.localScale = Vector3.one;
            }*/
        }

        public void Attack(Unit user, LayerMask targetLayerMask)
        {
            var recoilAngle = user.UnitData.Stats[StatType.RecoilAngle].Value;
            var damage = user.UnitData.Stats[StatType.AttackPoint].Value;

            if (!gunModel.ShootParticle.isPlaying)
                gunModel.ShootParticle.Play();

            Vector3 rotation = gunModel.Muzzle.eulerAngles;
            rotation.y += UnityEngine.Random.Range(-recoilAngle / 2, recoilAngle / 2);
            Bullet newBullet = BulletObjectPool.current.GetObject();// Instantiate(gunData.BaseData.Bullet, gunModel.Muzzle);
            newBullet.transform.position = gunModel.Muzzle.position;
            newBullet.transform.eulerAngles = rotation;
            newBullet.transform.SetParent(null);

            //TODO: 총알 속도도 외부에서?
            newBullet.Init(user, gunData.Stats[StatType.BulletSpeed].Value, targetLayerMask);
            newBullet.Depart();
            fireEvent?.Invoke();
            if(audioSource.isPlaying)
            {
                audioSource.time = 0f;
            }
            audioSource.pitch = Random.Range(.9f, 1.1f);
            audioSource.Play();
        }

        public void Stop()
        {
            gunModel.ShootParticle.Stop();
        }

        public void Interact(object interactor)
        {
            PlayerUnit unit = interactor as PlayerUnit;
            if(unit != null)
            {
                unit.Equip(this);
            }
        }

        public void SetDrop(bool value)
        {
            if(value)
                transform.SetParent(null);
            rigidbody.isKinematic = !value;
            collider.enabled = value;
        }

        public void SetHighlight(bool value)
        {
            if (value)
                highlightOnEvent?.Invoke();
            else
                highlightOffEvent?.Invoke();
        }
    }
}
