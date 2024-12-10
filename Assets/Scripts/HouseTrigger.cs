using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseTrigger : MonoBehaviour
{
    public GameObject spawnZone;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            spawnZone.GetComponent<ZombieSpawnPatrol>().canSpawn = true;
        }
    }
}
