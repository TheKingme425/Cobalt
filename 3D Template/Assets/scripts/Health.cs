using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float health, maxHealth;
    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;
    public Image healthBar;




    // Start is called before the first frame update
    public void Start()
    {
        health = maxHealth;
    }
    public void TakeDamage(float amount)
    {
        amount = health;
        OnPlayerDamaged?.Invoke();

        if (health <= 0)
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
        }
    }
}