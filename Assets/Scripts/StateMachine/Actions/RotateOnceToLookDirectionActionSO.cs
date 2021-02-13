using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu(menuName = "StateActions/RotateOnceToLookDirectionAction")]
    public class RotateOnceToLookDirectionActionSO : StateActionSO<RotateOnceToLookDirectionAction>
    {
        [SerializeField] private float speed;
        public float Speed => speed;
    }

    public class RotateOnceToLookDirectionAction : StateAction
    {
        private Transform targetTransform;
        private IEnumerator coroutine;
        private EventVector3 lookDirection;
        private MonoBehaviour mono;

        private RotateOnceToLookDirectionActionSO SO => so as RotateOnceToLookDirectionActionSO;

        protected override void OnInit(SystemBinder systemBinder)
        {
            targetTransform = systemBinder.GetSystem<Unit>().ModelRoot;
            lookDirection = systemBinder.GetSystem<MoveSystem>().LookDirection;
            mono = systemBinder.GetComponent<MonoBehaviour>();

            hasActivate = true;
            hasDeactivate = true;
        }

        public override void OnStateActivate()
        {
            coroutine = Rotate(lookDirection.Value);
            mono.StartCoroutine(coroutine);
        }

        public override void OnStateDeactivate()
        {
            if(coroutine != null)
            {
                mono.StopCoroutine(coroutine);
                coroutine = null;
            }
        }

        private IEnumerator Rotate(Vector3 targetVec)
        {
            while(Vector3.Distance(targetTransform.forward, targetVec) > .001f)
            {
                targetTransform.forward = Vector3.Slerp(targetTransform.forward, targetVec, SO.Speed * Time.deltaTime);
                yield return null;
            }

            targetTransform.forward = lookDirection.Value;
            coroutine = null;
        }
    }
}
