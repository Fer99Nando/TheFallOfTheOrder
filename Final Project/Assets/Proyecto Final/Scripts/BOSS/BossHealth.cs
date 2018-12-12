using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{ 
    public int startingHp;

    public int currentHp;

    public Slider healthSlider;

    public GameObject victory;

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

        // Animacion de muerte;
        Destroy(gameObject);
        victory.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
    }
}
