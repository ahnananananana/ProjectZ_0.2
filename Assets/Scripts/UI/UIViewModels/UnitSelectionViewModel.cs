using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace HDV
{
    [Serializable]
    public class UnitSelectionViewModel : UIViewModel<GameData>
    {
        private EventReferenceType<PlayerUnitBaseData> selectedUnit = new EventReferenceType<PlayerUnitBaseData>();
        private EventList<PlayerUnitBaseData> units = new EventList<PlayerUnitBaseData>();

        public EventReferenceType<PlayerUnitBaseData> SelectedUnit => selectedUnit;
        public EventList<PlayerUnitBaseData> Units => units;

        [SerializeField] private UnityEvent<UIUnit> selectUnitEvent;

        public void Init()
        {
            selectedUnit.Value = null;
        }

        protected override void OnBindModel(GameData model)
        {
            selectedUnit.ChangeEvent += model.SetPlayerUnit;

            units.AddRange(model.PlayerUnitBaseDatas);

            Init();
        }

        public void SelectUnit(UIUnit uiUnit)
        {
            selectedUnit.Value = uiUnit.Model;
            selectUnitEvent?.Invoke(uiUnit);
        }

    }
}
