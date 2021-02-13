using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace HDV
{
    public class EnemyStateModel : UnitStateModel, INavMeshMoveSystem
    {
        [SerializeField] private EnemyUnitData unitData;
        private EventObject<PlayerUnitStateModel> targetPlayer = new EventObject<PlayerUnitStateModel>();
        private EventFloat targetDistance = new EventFloat();

        public EventObject<PlayerUnitStateModel> TargetPlayer => targetPlayer;
        public EnemyUnitData UnitData => unitData;
        public Transform Target => targetPlayer.Value?.transform;
        public NavMeshMoveSystem MoveSystem => moveSystem as NavMeshMoveSystem;
        public EventFloat TargetDistance => targetDistance;

        protected override void Awake()
        {
            base.Awake();

            moveSystem = GetComponent<NavMeshMoveSystem>();

            unitData?.Init();
        }

        private void Update()
        {
            if (targetPlayer.Value != null)
                targetDistance.Value = (transform.position - targetPlayer.Value.transform.position).sqrMagnitude;
        }

        public override void OnDetectEnemyIn(Collider collider, Vector3 position)
        {
            var player = collider.GetComponent<PlayerUnitStateModel>();
            Debug.AssertIsNotNull(player);
            targetPlayer.Value = player;
        }

        public override void OnDetectEnemyOut(Collider collider, Vector3 position)
        {
            var player = collider.GetComponent<PlayerUnitStateModel>();
            Debug.AssertIsNotNull(player);
            targetPlayer.Value = null;
        }
    }
}
