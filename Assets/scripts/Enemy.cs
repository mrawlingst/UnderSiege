using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3;
    public int deathTime = 3;

    public bool dead = false;
    public int score = 1;

    public AudioClip[] swordHits;

    private AudioSource audioSource;
    
    //public GameObject manager;

	void Start ()
    {
        audioSource = GetComponent<AudioSource>();
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
            GetComponent<CapsuleCollider>().enabled = false;
        }

        if (other.gameObject.tag == "projectile")
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "sword")
        {
            playSwordHitSound();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("LEFT COLLIDED");
    }

    private void playSwordHitSound()
    {
        int soundIndex = (int)(Mathf.Floor(Random.Range(0.001f, 2.999f)) % swordHits.Length);
        audioSource.pitch = Random.Range(0.85f, 1.15f);
        audioSource.volume = Random.Range(0.30f, 0.40f);
        audioSource.clip = swordHits[soundIndex];
        audioSource.Play();
    }
}
