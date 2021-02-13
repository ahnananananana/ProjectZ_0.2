using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace HDV
{
    [CustomEditor(typeof(Sector)), CanEditMultipleObjects]
    public class SectorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Find Adjacency Sectors"))
            {
                for (int i = 0; i < targets.Length; ++i)
                {
                    if (targets[i] is Sector)
                    {
                        FindAdjacencySectors(targets[i] as Sector);
                        EditorUtility.SetDirty(targets[i]);
                    }
                }
            }
        }

        private void FindAdjacencySectors(Sector sector)
        {
            var center = sector.transform.position + sector.Collider.center;
            center.y += sector.Collider.size.y / 2 + 1;
            List<Sector> adjacencySectors = new List<Sector>();

            if (Check(center + new Vector3(sector.Collider.size.x / 2 + 1, 0, 0), out Sector outSector))
                adjacencySectors.Add(outSector);
            if (Check(center - new Vector3(sector.Collider.size.x / 2 + 1, 0, 0), out outSector))
                adjacencySectors.Add(outSector);
            if (Check(center + new Vector3(0, 0, sector.Collider.size.z / 2 + 1), out outSector))
                adjacencySectors.Add(outSector);
            if (Check(center - new Vector3(0, 0, sector.Collider.size.z / 2 + 1), out outSector))
                adjacencySectors.Add(outSector);

            if (Check(center + new Vector3(sector.Collider.size.x / 2 + 1, 0, sector.Collider.size.z / 2 + 1), out outSector))
                adjacencySectors.Add(outSector);
            if (Check(center - new Vector3(sector.Collider.size.x / 2 + 1, 0, sector.Collider.size.z / 2 + 1), out outSector))
                adjacencySectors.Add(outSector);
            if (Check(center + new Vector3(-(sector.Collider.size.x / 2 + 1), 0, sector.Collider.size.z / 2 + 1), out outSector))
                adjacencySectors.Add(outSector);
            if (Check(center + new Vector3(sector.Collider.size.x / 2 + 1, 0, -(sector.Collider.size.z / 2 + 1)), out outSector))
                adjacencySectors.Add(outSector);

            sector.AdjacencySectors = adjacencySectors.ToArray();
        }

        private bool Check(Vector3 from, out Sector outSector)
        {
            if (Physics.Raycast(new Ray(from, Vector3.down), out var hit, 2, LayerMask.GetMask("TriggerByPlayer")))
            {
                return hit.collider.TryGetComponent(out outSector);
            }
            outSector = null;
            return false;
        }
    }
}
