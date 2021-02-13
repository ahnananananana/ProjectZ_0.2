using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace HDV
{
    public class Sector : MonoBehaviour
    {
        [SerializeField] private new BoxCollider collider;
        [SerializeField] private Sector[] adjacencySectors;
        [SerializeField] private SpawnArea[] spawnAreas;

        [SerializeField] private SectorEvent playerInEvent, playerOutEvent;

        public Sector[] AdjacencySectors { get => adjacencySectors; set => adjacencySectors = value; }
        public BoxCollider Collider => collider;
        public SpawnArea[] SpawnAreas => spawnAreas; 

        private void Awake()
        {
            collider = GetComponent<BoxCollider>();
        }

        public Unit Spawn(UnitBaseData baseData)
        {
            var pos = transform.position + Random.insideUnitSphere;
            pos.y = 0f;

            var spawnedUnit = Instantiate(baseData.UnitPrefab, pos, Quaternion.identity);

            return spawnedUnit;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(name + " OnTriggerEnter " + other.name);
            playerInEvent?.Invoke(this);
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log(name + " OnTriggerExit " + other.name);
            playerOutEvent?.Invoke(this);
        }

        private bool isSelected;
        private void OnDrawGizmos()
        {
            isSelected = false;
            if (collider.enabled)
            {
                Gizmos.color = Color.green;
                GizmoUtil.DrawCollider(transform, collider);
                return;
            }

            for (int i = 0; i < adjacencySectors.Length; ++i)
            {
                if(adjacencySectors[i].isSelected)
                {
                    Gizmos.color = Color.yellow;
                    GizmoUtil.DrawCollider(transform, collider);
                    return;
                }
            }

            Gizmos.color = Color.red;
            GizmoUtil.DrawCollider(transform, collider);
        }

        private void OnDrawGizmosSelected()
        {
            isSelected = true;
            Gizmos.color = Color.green;
            GizmoUtil.DrawCollider(transform, collider);
        }
    }
}
