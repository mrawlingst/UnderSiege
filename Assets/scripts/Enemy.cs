using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;

	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        health -= 10;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("LEFT COLLIDED");
    }
}
