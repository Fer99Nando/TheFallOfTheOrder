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

    BossPrueba bossBehaviour;

    bool isDead;
    bool segundaFase;

    void Awake()
    {
        segundaFase = true;

        bossBehaviour.GetComponent<BossPrueba>();
        //anim = GetComponent<Animator>();
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

        // Sonido asignado del jugador

        if (currentHp <= 0 && !isDead && segundaFase == true)
        {
            segundaFase = false;
            bossBehaviour.ChangePhase();
            
        }
        else if (currentHp <= 0 && !isDead && segundaFase == false)
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
