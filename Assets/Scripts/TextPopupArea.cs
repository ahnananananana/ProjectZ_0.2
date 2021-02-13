using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class TextPopupArea : MonoBehaviour
    {
        [SerializeField] private TextPopup textPopupPrefab;
        [SerializeField] private RectTransform popupArea;
        private FollowingCamera cam;

        private void Awake()
        {
            cam = FindObjectOfType<FollowingCamera>();
        }

        public void PopupFloat(float value)
        {
            Vector3 pos = popupArea.position;
            pos.x += Random.Range(-popupArea.rect.width / 2, popupArea.rect.width / 2);
            var popup = TextPopupObjectPool.current.GetObject();//Instantiate(textPopupPrefab, pos, Quaternion.identity);
            popup.transform.position = pos;
            popup.transform.rotation = Quaternion.identity;
            popup.transform.forward = cam.transform.forward;
            popup.SetText(value.ToString());
        }
    }
}
