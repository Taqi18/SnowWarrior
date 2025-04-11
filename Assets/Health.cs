using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float health;
    [SerializeField] private float healthDamage;
    private bool _isAlive = true;

    public void SetHealth(float health)
    {
        this.health = health;
        healthSlider.value = health;

        if (health <= 0)
        {
            _isAlive = false;
            StartCoroutine(DeathAnimationDelay());
        }
    }

    private IEnumerator DeathAnimationDelay()
    {
        yield return new WaitForSeconds(4f);
        gameObject.SetActive(false);
    }

    public float GetHealth()
    {
        return health;
    }

    public bool isAlive()
    {
        return _isAlive;
    }

    public void Damage()
    {
        health -= healthDamage;
        SetHealth(health);
    }

 
}


