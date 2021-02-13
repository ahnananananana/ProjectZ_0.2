using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class UnitSoltsSorter : UIGridLayoutSorter<UnitSlot, PlayerUnitDataViewModel>
    {
        public void SortByName(bool isAscending)
        {
            views.Sort((i, j) =>
            {
                if (isAscending)
                {
                    return i.Model.UnitName.Value.CompareTo(j.Model.UnitName.Value);
                }
                else
                {
                    return j.Model.UnitName.Value.CompareTo(i.Model.UnitName.Value);
                }
            });

            for (int i = 0; i < views.Count; ++i)
                views[i].UIAnimator.SetActive(false);

            Sort();
        }

        protected override void OnMoveEnd()
        {
            for (int i = 0; i < views.Count; ++i)
                views[i].UIAnimator.SetActive(true);
        }

    }
}
