using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHp;

    public int currentHp;

    public Slider healthSlider;

    public Material mat1;
    public Material mat2;

    // Sonido muerte

    Animator anim;

    // Sonidos

    bool isDead;

    void Awake()
    {
        anim = GetComponent<Animator>();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerWeapon")
        {
            transform.GetComponent<MeshRenderer>().material = mat2;
        }
        else transform.GetComponent<MeshRenderer>().material = mat1;
    }

        void Death()
    {
        isDead = true;

        // Animacion de muerte;
        Destroy(gameObject);
    }
}
