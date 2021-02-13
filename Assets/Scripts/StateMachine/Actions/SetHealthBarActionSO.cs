using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu(menuName = "StateActions/SetHealthBarAction")]
    public class SetHealthBarActionSO : StateActionSO<SetHealthBarAction>
    {
        [SerializeField] private bool setOnActivate;

        public bool SetOnActivate => setOnActivate;
    }

    public class SetHealthBarAction : StateAction
    {
        private HealthBar healthBar;

        private SetHealthBarActionSO SO => (SetHealthBarActionSO)so;

        protected override void OnInit(SystemBinder systemBinder)
        {
            healthBar = systemBinder.GetSystem<HealthBar>();

            hasActivate = true;
            hasDeactivate = true;
        }

        public override void OnStateActivate()
        {
            healthBar.IsActive = SO.SetOnActivate;
        }

        public override void OnStateDeactivate()
        {
            healthBar.IsActive = !SO.SetOnActivate;
        }
    }
}

