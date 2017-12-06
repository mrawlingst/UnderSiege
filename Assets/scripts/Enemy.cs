using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3;
    public int deathTime = 3;

    public bool dead = false;
    public int score = 1;

    //public GameObject manager;

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
        var dmg = other.gameObject.GetComponent<Damage>();

        if (dmg)
        {
            health -= dmg.damage;
        }
        else
        {
            health -= 1;
        }

        if (health <= 0)
        {
            GetComponent<Animator>().Play("Death");
            Destroy(gameObject, 10);
            GameObject manager = GameObject.FindGameObjectWithTag("manager");
            manager.GetComponent<Manager>().addScore(score);
        }

        if (other.gameObject.name == "Orb")
        {
            Destroy(other.gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("LEFT COLLIDED");
    }
}
