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
    public Slider Health1;



    // Start is called before the first frame update
    public void Start()
    {
        health = maxHealth;
    }
    private void Update()
    {
        Health1.value = health; 
    }
    public void TakeDamage(float amount)
    {
        amount = health;
        health = amount;
        OnPlayerDamaged?.Invoke();

        if (health <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            GameObject.Find("DeathScreen").SetActive(true);
            GameObject.Find("Canvas").SetActive(false);
        }
    }
}