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
    CharacterController controller;

    PlayerBehaviour playerBehaviour;

    // Sonidos

    bool isDead;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        currentHp = startingHp;
        playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();;
    }

    public void TakeDamage(int amount)
    {
        
        currentHp -= amount;

        healthSlider.value = currentHp;

        // Sonido asignado del jugador

        if (currentHp <= 0 && !isDead)
        {
            controller.enabled = false;
            anim.SetBool("Death", true);
        }
    }

    public void SeAcaboElHit()
    {
        anim.ResetTrigger("Hit");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerWeapon" && playerBehaviour.chargeAttack == true)
        {
            anim.SetTrigger("Hit");
        }
    }

    public void Death()
    {
        isDead = true;
        Destroy(gameObject);
    }
}
