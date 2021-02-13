using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public abstract class UnitStateModel : StateModel, IAnimator, ILookAtTransform, ITransform
    {
        [SerializeField] private Animator animator;
        protected MoveSystem moveSystem;
        private readonly EventFloat movingSpeed = new EventFloat();
        private readonly EventVector3 movingDirection = new EventVector3();

        public Animator Animator => animator;
        public EventFloat MovingSpeed => movingSpeed;
        public EventVector3 MovingDirection => movingDirection;
        Transform ILookAtTransform.Transform => transform;
        EventVector3 ILookAtTransform.LookDirection => movingDirection;
        public Transform Transform => transform;


        //TODO: 다른 곳에서 초기화해야 Model은 순수한 데이터로만
        protected virtual void Awake()
        {
            moveSystem = GetComponent<MoveSystem>();
        }

        public abstract void OnDetectEnemyIn(Collider collider, Vector3 position);

        public abstract void OnDetectEnemyOut(Collider collider, Vector3 position);
    }
}
