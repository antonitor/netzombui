using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int health = 40;

    private int currentHealth;

    void Start()
    {
        currentHealth = health;
    }

    public void TakeDamage(int amount, int playerNumber)
    {
        currentHealth -= amount;
        //instantiate hit effect
        //play audio source

        if (currentHealth <= 0)
        {
            //play feedback
            Destroy(gameObject);
        }
    }
}
