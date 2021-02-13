using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class PlayerUnitDataViewModel : UIViewModel<PlayerUnitData>
    {
        private EventString unitName = new EventString();
        private EventSprite image = new EventSprite();
        private EventInt maxHealthPoint = new EventInt();
        private EventInt attackPoint = new EventInt();
        private EventInt defensePoint = new EventInt();

        public EventString UnitName => unitName;
        public EventSprite Image => image;

        public EventInt MaxHealthPoint => maxHealthPoint; 
        public EventInt AttackPoint => attackPoint;
        public EventInt DefensePoint => defensePoint;

        public PlayerUnitDataViewModel() { }

        public PlayerUnitDataViewModel(PlayerUnitData model)
        {
        }

        protected override void OnBindModel(PlayerUnitData model)
        {
            throw new System.NotImplementedException();
        }
    }
}
