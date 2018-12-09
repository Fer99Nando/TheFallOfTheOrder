using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
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

    private float timeBoss;

    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyBehaviour = GetComponent<EnemyBehaviour>();
        currentHp = startingHp;
    }

    private void Update()
    {
        timeBoss += Time.deltaTime;
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
        //if (currentHp <= 50)
        if (timeBoss >= 10 && timeBoss <= 15)
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

    private void OnDestroy()
    {
        // que se destruya el objeto
    }

    private IEnumerator PhaseTwo ()
    {
        Debug.Log("Recupero la vida");
        anim.SetBool("PhaseTwo", true);
        yield return new WaitForSeconds(5.0f);
        currentHp = 100;
    }
}
