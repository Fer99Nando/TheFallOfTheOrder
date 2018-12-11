using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{ 
    public int startingHp;

    public int currentHp;

    public Slider healthSlider;

    public GameObject victory;
    public GameObject lifeDeath;

    // Sonido muerte

    Animator anim;

    // Sonidos

    EnemyBehaviour enemyBehaviour;

    bool isDead;

    void Awake()
    {
        lifeDeath.SetActive(true);
        anim = GetComponent<Animator>();
        enemyBehaviour = GetComponent<EnemyBehaviour>();
        currentHp = startingHp;
        anim.SetBool("Death", false);
    }

    private void Start()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
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
        lifeDeath.SetActive(false);
        StartCoroutine(CoDeath());
    }

    IEnumerator CoDeath()
    {

        anim.SetBool("Death", true);
        yield return new WaitForSeconds (1.0f);
        Destroy(gameObject);
        victory.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
    }
}
