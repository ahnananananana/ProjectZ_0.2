
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    [CreateAssetMenu(menuName = "StateActions/SetLayerAction")]
    public class SetLayerActionSO : StateActionSO<SetLayerAction>
    {
        [SerializeField] private LayerMask layer;

        public LayerMask Layer => layer;
    }

    //TODO: 직접 데이터 수정ㄴ?
    public class SetLayerAction : StateAction
    {
        private SetLayerActionSO SO => so as SetLayerActionSO;

        private GameObject gob;
        private int layerValue;

        protected override void OnInit(SystemBinder systemBinder)
        {
            gob = systemBinder.GetSystem<Unit>().UnitModel.gameObject;
            layerValue = SO.Layer;

            hasActivate = true;
        }

        public override void OnStateActivate()
        {
            gob.layer = layerValue;
        }
    }
}
