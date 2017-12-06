using UnityEngine;
using UnityEngine.SceneManagement;

public class CastleManager : MonoBehaviour
{
    public int maxCastleHealth = 20;
    public int CastleHealth = 20;

    public Light light;
    public Color dyingLight = Color.red;

    private Color healthyLight;

    void Awake()
    {
        healthyLight = light.color;
        CastleHealth = maxCastleHealth;
    }

    public void TakeDamage(int damage)
    {
        CastleHealth -= damage;

        var newColor = Color.Lerp(dyingLight, healthyLight, ((float)CastleHealth / (float)maxCastleHealth));
        light.color = newColor;

        if (CastleHealth <= 0)
        {
            Debug.Log("???????????????");
            SceneManager.LoadScene(1);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemyattack")
        {
            Debug.Log("EnemyAttack");
            TakeDamage(other.GetComponent<Damage>().damage);
        }
    }
}
