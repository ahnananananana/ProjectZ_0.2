using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public enum RewardType
    {
        Weapon,
        Perk,
        Gold,
    }

    public class RewardBox : MonoBehaviour, IInteractable
    {
        [SerializeField] private RewardType rewardType;
        [SerializeField] private GunBaseData[] guns;
        [SerializeField] private Gold goldPrefab;
        [SerializeField] private int goldAmount;
        [SerializeField] private Transform tossPoint;
        [SerializeField] private float tossForce;
        [SerializeField] private Transform[] tossDirTFs;
        [SerializeField] private bool isOpened;
        private Animator animator;

        public Vector3 Position => transform.position;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void Interact(object interactor)
        {
            PlayerUnit unit = interactor as PlayerUnit;
            if(!isOpened && unit != null)
            {
                isOpened = true;
                animator.Play("RewardBox_Open");
                switch (rewardType)
                {
                    case RewardType.Weapon:
                        {
                            for(int i = 0; i < guns.Length; ++i)
                            {
                                var gun = Instantiate(guns[i].Prefab);
                                gun.transform.position = tossPoint.position;
                                gun.transform.rotation = tossPoint.rotation;
                                Transform tossDirTF = tossDirTFs[i % tossDirTFs.Length];
                                Toss(gun.Rigidbody, tossDirTF);
                            }
                            break;
                        }
                    case RewardType.Perk:
                        {
                            break;
                        }
                    case RewardType.Gold:
                        {
                            for(int i = 0; i < goldAmount; ++i)
                            {
                                var gold = Instantiate(goldPrefab);
                                gold.transform.position = tossPoint.position;
                                gold.transform.rotation = tossPoint.rotation;
                                Transform tossDirTF = tossDirTFs[i % tossDirTFs.Length];
                                Toss(gold.Rigidbody, tossDirTF);
                            }
                            break;
                        }
                }
            }
        }

        private void Toss(Rigidbody rigidbody, Transform tossTF)
        {
            var tossDir = (tossTF.position - tossPoint.position).normalized;
            rigidbody.AddForce(tossDir * tossForce, ForceMode.Impulse);
        }

        public void SetHighlight(bool value)
        {
        }
    }
}
