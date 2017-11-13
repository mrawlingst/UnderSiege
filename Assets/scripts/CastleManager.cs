using UnityEngine;
using UnityEngine.SceneManagement;

public class CastleManager : MonoBehaviour
{
    public int CastleHealth = 20;

    public void TakeDamage(int damage)
    {
        CastleHealth -= damage;

        if (CastleHealth <= 0)
        {
            SceneManager.LoadScene(1);
       } 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemyattack")
        {
            Debug.Log("EnemyAttack");
            TakeDamage(1);
        }
    }
}
