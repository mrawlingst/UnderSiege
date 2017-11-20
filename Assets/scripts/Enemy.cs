using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 25;
    public int deathTime = 3;

    public bool dead = false;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        if (health <= 0 && !dead)
        {
            dead = true;
            GetComponent<Animator>().Play("Death");
            Destroy(gameObject, deathTime);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        health -= 10;

        if (health <= 0)
        {
            GetComponent<Animator>().Play("Death");
            Destroy(gameObject, 10);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("LEFT COLLIDED");
    }
}
