using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace HDV
{
    [CreateAssetMenu(menuName = "StateActions/NavAgentMoveAction")]
    public class NavAgentMoveActionSO : StateActionSO<NavAgentMoveAction>
    {
        [SerializeField] private float searchInterval;
        public float SearchInterval => searchInterval;
    }

    public class NavAgentMoveAction : StateAction
    {
        private NavMeshMoveSystem navMeshMoveSystem;
        private EventObject<ITargetable> target;
        private float lastCalculateTime;
        private NavMeshPath currentPath;
        private int currentCornerIdx;
        private Stat moveSpeed;

        private NavAgentMoveActionSO SO => so as NavAgentMoveActionSO;

        protected override void OnInit(SystemBinder systemBinder)
        {
            navMeshMoveSystem = systemBinder.GetSystem<NavMeshMoveSystem>();
            var myUnit = systemBinder.GetSystem<Unit>();
            target = myUnit.Target;
            moveSpeed = myUnit.UnitData.Stats[StatType.MoveSpeed];

            hasActivate = true;
            hasDeactivate = true;
            hasUpdate = true;
        }

        public override void OnStateActivate()
        {
            navMeshMoveSystem.SetSpeed(moveSpeed.Value);
            navMeshMoveSystem.SetActive(true);
        }

        public override void OnStateDeactivate()
        {
            navMeshMoveSystem.SetActive(false);
        }

        public override void OnStateUpdate()
        {
            if (lastCalculateTime + SO.SearchInterval < Time.time && target.Value != null)
            {
                lastCalculateTime = Time.time;
                navMeshMoveSystem.SetDestination(target.Value.Transform.position);
            }
            /*if (lastCalculateTime + SO.SearchInterval < Time.time)
            {
                if(navMeshMoveSystem.TryCalculatePath(target.Value.Transform.position, out currentPath))
                {
                    currentCornerIdx = 1;
                    lastCalculateTime = Time.time;
                }   
            }
            else if(currentPath != null && currentCornerIdx < currentPath.corners.Length)
            {
                var destination = currentPath.corners[currentCornerIdx];
                var currentPosition = navMeshMoveSystem.MoveTo(destination, moveSpeed.Value);
                if (Vector3.Distance(destination, currentPosition) < .001f)
                    ++currentCornerIdx;
            }*/
        }


    }
}
