using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    List<GameObject> spawnPoint = new List<GameObject>();

    public GameObject enemy;
    public float startSpawnRate = 5f;
    public float spawnRate = 5f; float lastSpawn;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject point in GameObject.FindGameObjectsWithTag("SpawnPoint"))
        {
            spawnPoint.Add(point);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - lastSpawn >= spawnRate)
        {
            //Spawn enemy
            int rando = Random.Range(0, spawnPoint.Count);
            Instantiate(enemy, spawnPoint[rando].transform.position, Quaternion.identity);
            lastSpawn = Time.time;
        }
    }
}
