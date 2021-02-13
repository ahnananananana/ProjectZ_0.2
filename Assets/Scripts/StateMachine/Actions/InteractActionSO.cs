using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu(menuName = "StateActions/InteractAction")]
    public class InteractActionSO : StateActionSO<InteractAction>
    {
    }

    public class InteractAction : StateAction
    {
        private InteractDetectSystem detectSystem;
        private InputSystem inputSystem;
        private Unit unit;

        protected override void OnInit(SystemBinder systemBinder)
        {
            detectSystem = systemBinder.GetSystem<InteractDetectSystem>();
            inputSystem = systemBinder.GetSystem<InputSystem>();
            unit = systemBinder.GetSystem<Unit>();

            hasActivate = true;
        }

        public override void OnStateActivate()
        {
            detectSystem.Interact(unit);
            inputSystem.InteractInput.Value = false;
        }

    }
}
