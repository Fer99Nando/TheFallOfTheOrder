using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{ 
    public float startingHp;

    public float currentHp;

    //public Slider healthSlider;
    public Image healthSlider;

    public GameObject victory;

    // Sonido muerte

    //Animator anim;

    // Sonidos

    EnemyPrueba enemyBehaviour;

    bool isDead;

    void Awake()
    {
        //anim = GetComponent<Animator>();
        enemyBehaviour = GetComponent<EnemyPrueba>();
        currentHp = startingHp;
        healthSlider.fillAmount = currentHp / startingHp;
    }

    private void Start()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
    }

    private void Update()
    {
        TakeDamage();
    }

    public void TakeDamage()
    {
        healthSlider.fillAmount = currentHp / startingHp;
        //healthSlider.value = currentHp;

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
