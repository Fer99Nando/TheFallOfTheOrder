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

    bool isDead;

    void Awake()
    {
        anim = GetComponent<Animator>();
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

    void Death()s
    {
        isDead = true;

        // Animacion de muerte;
        Destroy(gameObject);
    }
}
