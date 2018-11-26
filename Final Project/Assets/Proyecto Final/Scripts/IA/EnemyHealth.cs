using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    // Activamos el gameobject GameOver
    public GameObject gameOver;

    public int startingHp;

    public int currentHp;

    private int damage;

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

        gameOver.SetActive(true);
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

        enemyBehaviour.enabled = false;

        gameOver.SetActive(true);
        
    }

        private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag ("Damage"))
        {
            damage = 20;
        }
    }

    private void OnDestroy()
    {
        // que se destruya el objeto
    }
}
