using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public GameObject bossToSpawn;
    public float killsToSpawnBoss = 30;
    public float spawnTime = 3f;
    public int maxEnemy = 20;

    private int spawned = 0;

	void Start ()
    {
        InvokeRepeating("SpawnEnemy", spawnTime, spawnTime);
	}
	
	void Update ()
    {
		
	}

    void SpawnEnemy()
    {
        if (spawned < killsToSpawnBoss && !GameObject.FindGameObjectWithTag("boss"))
        {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length > maxEnemy - 1)
                return;

            var enemy = GameObject.Instantiate(enemyToSpawn, transform.position, transform.rotation);
            spawned++;
        }
        else if (spawned >= killsToSpawnBoss && !GameObject.FindGameObjectWithTag("boss"))
        {
            var boss = GameObject.Instantiate(bossToSpawn, transform.position, transform.rotation);
            spawned = 0;
        }
    }
}
