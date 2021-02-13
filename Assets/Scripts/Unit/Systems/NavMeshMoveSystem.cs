using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace HDV
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavMeshMoveSystem : MoveSystem
    {
        private NavMeshAgent agent;
        private NavMeshPath currentPath;
        private int currentCorner;

        protected override void OnAwake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        public void SetSpeed(float value) => agent.speed = value;

        protected override void OnMove(Vector3 direction, float speed)
        {
            agent.Move(direction * speed * Time.deltaTime);
        }

        public bool TryCalculatePath(Vector3 target, out NavMeshPath newPath)
        {
            newPath = new NavMeshPath();
            if (agent.CalculatePath(target, newPath))
            {
                currentPath = newPath;
                currentCorner = 0;
                return true;
            }
            return false;
        }

        public void SetDestination(Vector3 destination) => agent.SetDestination(destination);

        private void Update()
        {
            if(agent.velocity.normalized != Vector3.zero)
                moveDirection.Value = agent.velocity.normalized;
            moveSpeed.Value = agent.velocity.magnitude;
        }

        public void SetActive(bool value) => agent.enabled = value;

        /*private void OnDrawGizmos()
        {
            if(currentPath != null && currentPath.status == NavMeshPathStatus.PathComplete)
            {
                Gizmos.color = Color.red;
                for (int i = 0; i < currentPath.corners.Length; ++i)
                {
                    Gizmos.DrawSphere(currentPath.corners[i], 1f);
                }
            }
        }*/
    }
}
