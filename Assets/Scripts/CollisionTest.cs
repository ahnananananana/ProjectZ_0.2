using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(name + " OnTriggerEnter");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(name + " OnTriggerExit");
    }
}
