using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HDV
{
    public class GunCompareData
    {
        private Gun otherGun;
        private Gun playerGun;

        public GunCompareData(Gun playerGun, Gun otherGun)
        {
            this.playerGun = playerGun;
            this.otherGun = otherGun;
        }

        public Gun PlayerGun => playerGun;
        public Gun OtherGun => otherGun;
    }

    public class GunInfoUIManager : MonoBehaviour
    {
        private Camera mainCam;
        private Gun currentHightlightGun;
        private PlayerUnit playerUnit;
        [SerializeField] private RectTransform gunInfoPanel;

        [SerializeField] private GunCompareEvent openEvent, closeEvent;

        private void Awake()
        {
            mainCam = Camera.main;
            playerUnit = FindObjectOfType<PlayerUnit>();
        }

        public void SetHighlight(Gun gun)
        {
            if (currentHightlightGun == gun)
            {
                currentHightlightGun = null;
                StopAllCoroutines();
                closeEvent?.Invoke(null);
            }
            else
            {
                if (playerUnit == null)
                    playerUnit = FindObjectOfType<PlayerUnit>();
                if (playerUnit == null)
                    return;

                currentHightlightGun = gun;
                StartCoroutine(FixPanel());
                openEvent?.Invoke(new GunCompareData(playerUnit.CurrentGun, gun));
            }
        }

        private IEnumerator FixPanel()
        {
            while (currentHightlightGun != null)
            {
                var uiPos = mainCam.WorldToScreenPoint(currentHightlightGun.UIInfoPosition);
                gunInfoPanel.anchoredPosition = uiPos;
                yield return null;
            }
        }
    }
}
