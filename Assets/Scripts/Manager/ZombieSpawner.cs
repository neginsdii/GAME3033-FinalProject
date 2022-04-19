using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public int numZombiesToSpawn;
    public GameObject[] zombiePrefabs;
    public SpawnerVolume[] spawnerVolumes;

    GameObject followGameObject;
    public bool Spawned = false;
    // Start is called before the first frame update
    void Start()
    {
        followGameObject = GameObject.FindGameObjectWithTag("Player");

      
    }
	private void Update()
	{
		if(Vector3.Distance( followGameObject.transform.position, transform.position)<=30)
		{
            SpawnZombie();
		}
	}
	void SpawnZombie()
    {
        if (!Spawned)
    {
        for (int i = 0; i < numZombiesToSpawn; i++)
        {
            GameObject zombieToSpawn = zombiePrefabs[Random.Range(0, zombiePrefabs.Length)];
            SpawnerVolume spawnVolume = spawnerVolumes[Random.Range(0, spawnerVolumes.Length)];

            if (!followGameObject) return;

            GameObject zombie = Instantiate(zombieToSpawn, spawnVolume.GetPositionInBounds(), spawnVolume.transform.rotation);

            zombie.GetComponent<ZombieComponent>().Initialize(followGameObject);
        }
            Spawned = true;
        }
    }
}