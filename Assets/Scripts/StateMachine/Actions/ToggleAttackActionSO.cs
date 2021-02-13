using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HDV
{
    [CreateAssetMenu(menuName = "StateActions/ToggleAttackAction")]
    public class ToggleAttackActionSO : StateActionSO<ToggleAttackAction>
    {
    }

    public class ToggleAttackAction : StateAction
    {
        private ToggleAttackActionSO SO => so as ToggleAttackActionSO;

        private PlayerUnit playerUnit;
        private float lastAttackTime;

        protected override void OnInit(SystemBinder systemBinder)
        {
            playerUnit = systemBinder.GetSystem<PlayerUnit>();

            hasUpdate = true;
            hasDeactivate = true;
        }

        //TODO: aimPoint가 다 도달했을때 쏘게
        public override void OnStateUpdate()
        {
            if (lastAttackTime + 1 / playerUnit.UnitData.Stats[StatType.AttackSpeed].Value <= Time.time)
            {
                lastAttackTime = Time.time;
                playerUnit.CurrentGun.Attack(playerUnit, playerUnit.TargetLayerMask);
            }
        }

        public override void OnStateDeactivate()
        {
            playerUnit.CurrentGun.Stop();
        }

    }
}
