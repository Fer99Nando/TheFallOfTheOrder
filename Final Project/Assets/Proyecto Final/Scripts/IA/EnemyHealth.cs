using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHp;

    public int currentHp;

    private int damage;

    public Slider healthSlider;

    public Image damageImage;

    // Sonido muerte

    Animator anim;

    // Sonidos

    EnemyBehaviour playerControl;

    bool isDead;
    bool damaged;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerControl = GetComponent<EnemyBehaviour>();
        currentHp = startingHp;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeDamage(int amount)
    {
        damaged = true;

        damage = amount;
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

        playerControl.enabled = false;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Damage"))
        {
            damage = 10;
        }
    }

    private void OnDestroy()
    {
        // que se destruya el objeto
    }
}
