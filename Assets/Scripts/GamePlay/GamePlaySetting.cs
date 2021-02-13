using System;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{

    [CreateAssetMenu]
    public class GamePlaySetting : ScriptableObject
    {
        //TODO: 난이도 정보 저장
        [SerializeField] private float increaseEnemyNumInterval;
        [SerializeField] private int startEnemyNum;

        public float IncreaseEnemyNumInterval { get => increaseEnemyNumInterval; set => increaseEnemyNumInterval = value; }
        public int StartEnemyNum { get => startEnemyNum; set => startEnemyNum = value; }
    }
}
