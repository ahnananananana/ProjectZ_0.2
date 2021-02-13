using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class PlayerUnitStateModel : UnitStateModel
    {
        [SerializeField] protected PlayerUnitData unitData;
        [SerializeField] private FollowingCamera followingCamera;
        private readonly EventVector3 screenYDirection = new EventVector3();
        private readonly EventVector2 moveInput = new EventVector2();
        private EventList<EnemyStateModel> detectedEnemys = new EventList<EnemyStateModel>();
        private readonly EventFloat targetDistance = new EventFloat();

        public PlayerUnitData UnitData => unitData;
        public EventVector3 ScreenYDirection => screenYDirection;
        public EventVector2 MoveInput => moveInput;
        public CharacterControllerMoveSystem MoveSystem => moveSystem as CharacterControllerMoveSystem;
        public EventList<EnemyStateModel> DetectedEnemys => detectedEnemys;
        public EventObject<Transform> Target => throw new System.NotImplementedException();
        public EventFloat TargetDistance => targetDistance;

        protected override void Awake()
        {
            base.Awake();

            unitData.Init();
            followingCamera.SetTarget(transform);
            followingCamera.ScreenYDir.ChangeEvent += screenYDirection.SetValue;
        }

        public override void OnDetectEnemyIn(Collider collider, Vector3 position)
        {
            var enemy = collider.GetComponent<EnemyStateModel>();
            Debug.AssertIsNotNull(enemy);
            detectedEnemys.Add(enemy);
        }

        public override void OnDetectEnemyOut(Collider collider, Vector3 position)
        {
            var enemy = collider.GetComponent<EnemyStateModel>();
            Debug.AssertIsNotNull(enemy);
            detectedEnemys.Remove(enemy);
        }
    }
}

