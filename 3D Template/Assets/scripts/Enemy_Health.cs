using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public float health = 20f;
    public bool IsDead = false;
    
    void Update()
    {
        /*if(shot.shot == true)
        {
            
        }
        */
        if (health <= 0)
        {
            IsDead = true;
        }

        if (IsDead == true)
        {
            GameObject.Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Enemy took damage: " + damage);
        if (health <= 0)
        {
            IsDead = true;
        }
    }
}
