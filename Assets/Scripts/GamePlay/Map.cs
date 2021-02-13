using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDV
{
    public class Map : MonoBehaviour
    {
        [SerializeField] private Sector[] enemySpawners;
        [SerializeField] private Transform[] startPoints;

        public Sector[] EnemySpawners => enemySpawners;

        public Transform[] StartPoints => startPoints;
    }
}
