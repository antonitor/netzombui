using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int health = 40;

    [SerializeField]
    private ParticleSystem hitEffect;

    private int currentHealth;

    void Start()
    {
        currentHealth = health;
    }

    public void TakeDamage(Vector3 impactPoint, int amount, int playerNumber)
    {
        currentHealth -= amount;
        hitEffect.transform.position = impactPoint;
        hitEffect.Play(true);
        //instantiate hit effect
        //play audio source

        if (currentHealth <= 0)
        {
            //play feedback
            Destroy(gameObject);
        }
    }
}
