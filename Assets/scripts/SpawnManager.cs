using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyToSpawn;

	void Start ()
    {
        InvokeRepeating("SpawnEnemy", 3f, 3f);
	}
	
	void Update ()
    {
		
	}

    void SpawnEnemy()
    {
        
        var enemy = GameObject.Instantiate(enemyToSpawn, transform.position, transform.rotation);
    }
}
