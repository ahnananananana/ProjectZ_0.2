using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu(menuName = "StateActions/RotateToLookDirectionAction")]
    public class RotateToLookDirectionActionSO : StateActionSO<RotateToLookDirectionAction>
    {
        [SerializeField] private float speed;
        public float Speed => speed;
    }

    //TODO: 직접 데이터 수정ㄴ?
    public class RotateToLookDirectionAction : StateAction
    {
        private RotateToLookDirectionActionSO SO => so as RotateToLookDirectionActionSO;

        private Transform targetTransform;
        private EventVector3 lookDirection;

        protected override void OnInit(SystemBinder systemBinder)
        {
            targetTransform = systemBinder.GetSystem<Unit>().ModelRoot;
            lookDirection = systemBinder.GetSystem<MoveSystem>().LookDirection;

            hasUpdate = true;
            hasActivate = true;
        }

        public override void OnStateActivate()
        {
            Rotate();
        }

        public override void OnStateUpdate()
        {
            Rotate();
        }

        private void Rotate()
        {
            //TODO: lookDirection을 초기화해야
            if (lookDirection.Value == Vector3.zero)
                return;

            if ((targetTransform.forward - lookDirection.Value).sqrMagnitude > .001f)
            {
                targetTransform.forward = Vector3.Slerp(targetTransform.forward, lookDirection.Value, SO.Speed * Time.deltaTime);
            }
            else
            {
                targetTransform.forward = lookDirection.Value;
            }
        }
    }
}
