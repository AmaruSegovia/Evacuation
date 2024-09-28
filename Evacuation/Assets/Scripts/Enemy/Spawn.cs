using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Transform xRangeLeft;
    public Transform xRangeRight;

    public Transform yRangeUp;
    public Transform yRangeDown;

    public GameObject[] enemies;

    [SerializeField] float timeSpawn = 1;//En el segundo 1 vamos a respawnear un enemigo
    public float repeatSpawnRate = 2;//Cada 2 segundos vamos a respawnear otros enemigos

    public Transform player;
    void Start()
    {
        InvokeRepeating("SpawnEnemies", timeSpawn, repeatSpawnRate);
    }
    public void SpawnEnemies()
    {
        Vector3 spawnPosition = new Vector3(0, 0, 0);
        spawnPosition = new Vector3(Random.Range(xRangeLeft.position.x, xRangeRight.position.x), Random.Range(yRangeDown.position.y, yRangeUp.position.y), 0);
        GameObject enemie = Instantiate(enemies[0], spawnPosition, gameObject.transform.rotation);

        AIDestinationSetter aIDestinationSetter = enemie.GetComponent<AIDestinationSetter>();
        if (aIDestinationSetter != null)
        {
            aIDestinationSetter.target = player;
        }
    }
}