using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{ 
    public float startingHp;

    public float currentHp;

    public Image healthSlider;

    //public GameObject victory;
    public GameObject victoryObject;
    public GameObject desactivarHud;

    CharacterController controller;

    int phase;

    // Sonido muerte

    Animator anim;

    // Sonidos

    BossPrueba bossBehaviour;

    public bool isDead;
    bool segundaFase;

    void Awake()
    {
        victoryObject.SetActive(false);
        segundaFase = true;

        bossBehaviour = GetComponent<BossPrueba>();
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
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
        switch (phase)
        {
            case 0:
                PhaseOne();
                break;
            case 1:
                PhaseTwo();
                break;
            default:
                break;
        }
    }

    public void TakeDamage(int amount)
    {
        currentHp -= amount;

        healthSlider.fillAmount = currentHp / startingHp;
    }

    void PhaseOne()
    {
        if (currentHp <= 0 && !isDead && segundaFase == true)
        {
            //controller.enabled = false;

            currentHp = startingHp;
            healthSlider.fillAmount = currentHp / startingHp;

            bossBehaviour.ChangePhase();

            segundaFase = false;
            phase = 1;

        }
    }

    void PhaseTwo()
    {
        if (currentHp <= 0 && !isDead && segundaFase == false)
        {
            isDead = true;
            if (isDead)
            {
                anim.SetBool("Death", true);
            }
        }
    }

    void Death()
    {
        controller.enabled = false;
        victoryObject.SetActive(true);
        desactivarHud.SetActive(false);
        Cursor.visible = true;
    }
}
