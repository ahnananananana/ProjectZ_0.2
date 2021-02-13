using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HDV
{
    public abstract class Unit : MonoBehaviour, IStateComponent, ITargetable, IStateObject, IHittable, IAttackable
    {
        [SerializeField] protected bool initOnAwake;
        [SerializeField] private LayerMask targetLayerMask;
        [SerializeField] protected StateMachine stateMachine;
        [SerializeField] protected SystemBinder systemBinder;
        [SerializeField] private Transform targetPoint, aimPoint;
        [SerializeField] private Transform modelRoot;
        [SerializeField] private ParticleSystem hitParticlePrefab;
        [SerializeField] private AudioClip hitAudioClip;

        [SerializeField] private UnityEvent attackEnableEvent, attackDisableEvent;
        [SerializeField] private UnitEvent deadEvent;
        [SerializeField] private FloatEvent damagedEvent;
        [SerializeField] protected StatEvent healthChangeEvent;

        public event Action<ITargetable> DisableEvent;
        public abstract UnitData UnitData { get; }
        public abstract UnitModel UnitModel { get; }
        public abstract EventObject<ITargetable> Target { get; }
        public abstract EventFloat TargetDistance { get; }
        public Transform Transform => transform;
        public Vector3 TargetPoint => targetPoint.position;
        public Transform AimPoint => aimPoint;
        public LayerMask TargetLayerMask => targetLayerMask;
        public Transform ModelRoot => modelRoot; 

        protected abstract void OnInit();

        public float TakeDamage(DamageData damageData, ContactData contactData)
        {
            float damage = damageData.Damage - UnitData.Stats[StatType.DefensePoint].Value;
            if (damage < 0f)
                return 0f;

            var audio = damageData.Source as IAudioSource;
            if(audio != null)
            {
                audio.AudioSource.clip = hitAudioClip;
                audio.AudioSource.Play();
            }

            var healthPoint = (StatCurrent)UnitData.Stats[StatType.HealthPoint];
            if (damage > 0f)
            {
                healthPoint.ModifyCurrentValue(damage, Operation.SUB);
                var hitParticle = ParticleObjectPool.current.GetObject();
                hitParticle.transform.position = contactData.Point;
                hitParticle.transform.rotation = Quaternion.LookRotation(contactData.Normal);
                damagedEvent?.Invoke(damage);
            }

            if(healthPoint.CurrentValue <= 0)
            {
                deadEvent?.Invoke(this);
                DisableEvent?.Invoke(this);
            }

            return damage;
        }

        public void OnAttackHit(object source, ContactData contactData)
        {
            if(contactData.OtherCollider.TryGetComponent<IHittable>(out var hittable))
            {
                DamageData damageData = new DamageData(source, UnitData.Stats[StatType.AttackPoint].Value);
                float givenDamage = hittable.TakeDamage(damageData, contactData);
            }
        }

        public void OnAttackEnable()
        {
            attackEnableEvent?.Invoke();
        }

        public void OnAttackDisable()
        {
            attackDisableEvent?.Invoke();
        }
    }

    public abstract class Unit<D, M> : Unit
        where D : UnitData
        where M : UnitModel
    {
        [SerializeField] protected D unitData;
        [SerializeField] protected M unitModel;

        public override UnitData UnitData => unitData;
        public override UnitModel UnitModel => unitModel;

        private void Awake()
        {
            if (initOnAwake && unitData != null)
                Init(unitData);
        }

        public void Init(D unitData = null)
        {
            Debug.Assert(unitData != null || this.unitData != null);
            if (unitData == null)
                unitData = this.unitData;
            else
                this.unitData = unitData;

            if (!unitData.IsInit)
                unitData.Init();

            systemBinder.Init();

            OnInit();

            stateMachine.Init(this, systemBinder);

            if(healthChangeEvent != null)
            {
                var stat = (StatCurrent)unitData.Stats[StatType.HealthPoint];
                stat.ChangeCurrentValueEvent += (_) => healthChangeEvent.Invoke(stat);
                stat.ChangeValueEvent += (_) => healthChangeEvent.Invoke(stat);
            }
        }
    }
}
