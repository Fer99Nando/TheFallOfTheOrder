using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
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

    void ChangeBoss()
    {
        if (currentHp <= 50)
        {
            StartCoroutine(PhaseTwo());
        }
    }

    void Death()
    {
        isDead = true;

        // Animacion de muerte;

        enemyBehaviour.enabled = false;    
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

    private IEnumerator PhaseTwo ()
    {
        anim.SetBool("PhaseTwo", true);
        yield return new WaitForSeconds(5.0f);
        currentHp = 100;
    }
}
