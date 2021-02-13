using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HDV
{
    public class GamePlayManager : MonoBehaviour
    {
        [SerializeField] private GameData gameData;
        [SerializeField] private GamePlaySetting gameSetting;
        [SerializeField] private FollowingCamera followingCamera;
        [SerializeField] private Transform[] startPoints;
        /*[SerializeField] private Sector[] startSectors;
        [SerializeField] private List<SpawnArea> spawnSectors = new List<SpawnArea>();*/
        [SerializeField] private float spawnDistance, spawninterval;
        private int maxEnemyNumber;
        //private List<Sector> currentSectors = new List<Sector>();
        private List<EnemyUnit> enemyUnits = new List<EnemyUnit>();
        private PlayerUnit playerUnit;
        private int timeRecordSecond;
        [SerializeField] private TMPro.TMP_Text timeRecordText;

        [SerializeField] private UnityEvent playerDeadEvent, resetEvent;

        private void Start()
        {
            ResetGame();
        }

        private void DeployPlayer(PlayerUnitData playerUnitData)
        {
            playerUnit = Instantiate(playerUnitData.BaseData.UnitPrefab);
            Transform startPoint = startPoints[Random.Range(0, startPoints.Length)];

            playerUnit.transform.position = startPoint.position;
            playerUnit.transform.rotation = startPoint.rotation;

            playerUnit.FollowingCamera = followingCamera;
            
            playerUnit.Init(playerUnitData);
            //startSector.Collider.enabled = true;
        }

        private void StartPlay()
        {
            StartCoroutine(StartTimeRecord());
            StartCoroutine(SpawnEnemys());
            StartCoroutine(IncreaseSpawnNumber());
        }

        public void ResetGame()
        {
            maxEnemyNumber = gameSetting.StartEnemyNum;
            for (int i = 0; i < enemyUnits.Count; ++i)
                Destroy(enemyUnits[i].gameObject);
            enemyUnits.Clear();
            if(playerUnit != null)
            {
                Destroy(playerUnit.gameObject);
                playerUnit = null;
            }
            timeRecordSecond = 0;
            resetEvent?.Invoke();

            DeployPlayer(new PlayerUnitData(gameData.PlayerUnitData.Value));
            StartPlay();
        }

        /*public void ActivateSector(Sector sector = null)
        {
            if(sector != null)
            {
                //TODO: 중복검사하지말아야
                if (currentSectors.Contains(sector))
                    return;
                sector.Collider.enabled = true;
                currentSectors.Add(sector);
            }

            for(int i = 0; i < currentSectors.Count; ++i)
            {
                var currentSector = currentSectors[i];
                currentSector.Collider.enabled = true;
                for (int j = 0; j < currentSector.AdjacencySectors.Length; ++j)
                {
                    currentSector.AdjacencySectors[j].Collider.enabled = true;
                }
            }

            spawnSectors.Clear();
            for (int i = 0; i < currentSectors.Count; ++i)
            {
                var currentSector = currentSectors[i];
                for (int j = 0; j < currentSector.AdjacencySectors.Length; ++j)
                {
                    var adjSector = currentSector.AdjacencySectors[j];
                    for (int k = 0; k < adjSector.AdjacencySectors.Length; ++k)
                    {
                        if (adjSector.AdjacencySectors[k].Collider.enabled == false && adjSector.AdjacencySectors[k].SpawnAreas != null)
                            spawnSectors.AddRange(adjSector.AdjacencySectors[k].SpawnAreas);
                    }
                }
            }
        }

        public void DeactivateSector(Sector sector)
        {
            if (!currentSectors.Remove(sector))
            {
                Debug.LogWarning("Not activated sector " + sector.name);
                return;
            }

            //TODO: 중복 enable 개선해야
            sector.Collider.enabled = false;
            for(int i = 0; i < sector.AdjacencySectors.Length; ++i)
            {
                sector.AdjacencySectors[i].Collider.enabled = false;
            }

            ActivateSector();
        }

        private bool TryGetSpawnPosition(out Vector3 spawnPosition)
        {
            spawnPosition = default;
            if (spawnSectors.Count == 0)
                return false;

            spawnPosition = spawnSectors[Random.Range(0, spawnSectors.Count)].GetRandomSpawnPosition();
            return true;
        }*/

        public void OnEnemyDead(Unit unit)
        {
            var enemyUnit = unit as EnemyUnit;
            Debug.AssertIsNotNull(enemyUnit);
            if(enemyUnits.Remove(enemyUnit))
            {
            }
        }

        public void OnPlayerDead(Unit _)
        {
            StopAllCoroutines();
            StartCoroutine(PlayDeadEvent());
        }

        private IEnumerator PlayDeadEvent()
        {
            yield return new WaitForSeconds(3f);
            playerDeadEvent?.Invoke();
        }

        private IEnumerator SpawnEnemys()
        {
            while(true)
            {
                if(enemyUnits.Count < maxEnemyNumber)
                {
                    while(enemyUnits.Count < maxEnemyNumber)
                    {
                        var randomValue = Random.insideUnitCircle.normalized * spawnDistance;
                        Vector3 spawnPoint = playerUnit.transform.position + new Vector3(randomValue.x, 0, randomValue.y);
                        var enemyBaseData = gameData.EnemyUnitBaseDatas[Random.Range(0, gameData.EnemyUnitBaseDatas.Length)] as EnemyUnitBaseData;
                        var enemy = Instantiate(enemyBaseData.UnitPrefab, spawnPoint, Quaternion.identity);
                        enemy.Target.Value = playerUnit;
                        enemy.TargetDistance.Value = Vector3.Distance(enemy.transform.position, playerUnit.transform.position);
                        enemyUnits.Add(enemy);
                    }
                }

                yield return new WaitForSeconds(spawninterval);
            }
        }

        private IEnumerator IncreaseSpawnNumber()
        {
            while(true)
            {
                yield return new WaitForSeconds(gameSetting.IncreaseEnemyNumInterval);
                ++maxEnemyNumber;
            }
        }

        private IEnumerator StartTimeRecord()
        {
            while(true)
            {
                int min = timeRecordSecond / 60;
                int sec = timeRecordSecond % 60;
                string minText = min > 9 ? min.ToString() : ("0" + min);
                string secText = sec > 9 ? sec.ToString() : ("0" + sec);
                timeRecordText.text = minText + " : " + secText;
                yield return new WaitForSeconds(1f);
                ++timeRecordSecond;
            }
        }

    }
}
