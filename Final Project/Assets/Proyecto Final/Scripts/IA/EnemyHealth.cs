using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHp;

    public int currentHp;

    public Slider healthSlider;

    // Sonido muerte

    Animator anim;

    // Sonidos

    EnemyBehaviour enemyBehaviour;

    bool isDead;

    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyBehaviour = GetComponent<EnemyBehaviour>();
        currentHp = startingHp;
    }

    public void TakeDamage(int amount)
    {
        currentHp -= amount;

        healthSlider.value = currentHp;

        // Sonido asignado del jugador

        if (currentHp <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        // Animacion de muerte;
        Destroy(gameObject);   
    }
}
