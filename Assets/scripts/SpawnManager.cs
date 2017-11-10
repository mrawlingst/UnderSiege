using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyToSpawn;
    public float spawnTime = 3f;
    public int maxEnemy = 20;

	void Start ()
    {
        InvokeRepeating("SpawnEnemy", spawnTime, spawnTime);
	}
	
	void Update ()
    {
		
	}

    void SpawnEnemy()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > maxEnemy - 1)
            return;

        var enemy = GameObject.Instantiate(enemyToSpawn, transform.position, transform.rotation);
    }
}
