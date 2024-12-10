using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnPatrol : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] zombies;
    public int zombieSpawnAmt = 3;

    private float reSpawnTimer = 10;
    private float resetTimer = 0;
    [HideInInspector]
    public bool canSpawn = true;

    public bool houseSpawn = false;
    [HideInInspector]
    public bool spawnForward = true;
    public GameObject goingForward;
    public GameObject goingBack;


    private void Start()
    {
        if(houseSpawn == true)
        {
            canSpawn = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (canSpawn == false && houseSpawn == false)
        {
            resetTimer += 1 * Time.deltaTime;
            if(resetTimer >= reSpawnTimer)
            {
                canSpawn = true;
                resetTimer = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(spawnForward == true)
        {
            spawnPoints = goingForward.GetComponent<SpawnDirection>().targetList;
        }
        if (spawnForward == false)
        {
            spawnPoints = goingBack.GetComponent<SpawnDirection>().targetList;
        }
        if (other.CompareTag("Player") && canSpawn == true && SaveScript.zombiesInGameAmt < 140 - zombieSpawnAmt)
        {
            SpawnZombies();
        }
        if (other.CompareTag("Player") && canSpawn == true && SaveScript.zombiesInGameAmt >= 140 - zombieSpawnAmt)
        {
            GameObject[] zombiesToDestroy = GameObject.FindGameObjectsWithTag("zombie");
            for(int i = 0; i < zombieSpawnAmt; i++)
            {
                if (zombiesToDestroy.Length >= zombieSpawnAmt)
                {
                    float furthestDistance = Vector3.Distance(transform.position, zombiesToDestroy[i].transform.position);
                    if (furthestDistance > 30)
                    {
                        Destroy(zombiesToDestroy[i]);
                    }
                }
            }
            SpawnZombies();
        }

    } 
    
    void SpawnZombies()
    {
        for (int i = 0; i < zombieSpawnAmt; i++)
        {
            if (houseSpawn == false)
            {
                int spawnRandom = Random.Range(0, spawnPoints.Length);
                Instantiate(zombies[Random.Range(0, zombies.Length)], new Vector3(spawnPoints[spawnRandom].position.x - Random.Range(0, 10), spawnPoints[spawnRandom].position.y, spawnPoints[spawnRandom].position.z - Random.Range(0, 5)), spawnPoints[spawnRandom].rotation);
            }
            else
            {
                Instantiate(zombies[Random.Range(0, zombies.Length)], spawnPoints[i].position, spawnPoints[i].rotation);
            }
            SaveScript.zombiesInGameAmt++;
        }
        canSpawn = false;
    }
}
