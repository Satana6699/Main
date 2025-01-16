using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float trapDamage = 1f;
    [SerializeField] private GameManager gameManager;
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Death();
        }

        UpdateHealthSlider();
    }

    private void Death()
    {
        health = 0;
        Destroy(gameObject);
        gameManager.RestartLevel();
    }

    private void UpdateHealthSlider()
    {
        healthSlider.value = health;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Trap"))
        {
            TakeDamage(trapDamage);
        }
        else if (other.CompareTag("DeadZone"))
        {
            Death();
        }
    }
}
