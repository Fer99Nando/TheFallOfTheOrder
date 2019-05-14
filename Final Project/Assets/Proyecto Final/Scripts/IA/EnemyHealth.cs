using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHp;

    public int currentHp;

    public Slider healthSlider;

    private NavMeshAgent agent;

    public AudioSource mouthSounds;
    public AudioClip hitSound;

    Animator anim;
    CharacterController controller;

    PlayerBehaviour playerBehaviour;

    public GameObject[] bloodPart;
    public GameObject SpawnDamage;
    GameObject currentBlood;
    private GameObject bloodinsta;
    int index;

    // Sonidos

    public bool isDead;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        currentHp = startingHp;
        playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        healthSlider.value = currentHp;
    }

    public void TakeDamage(int amount)
    {
        
        currentHp -= amount;

        healthSlider.value = currentHp;

        // Sonido asignado del jugador

        if (currentHp <= 0 && !isDead)
        {
            isDead = true;
            agent.isStopped = true;
            controller.enabled = false;
            anim.SetBool("Death", true);
        }

        Debug.Log("DAÑO AL ZOMBIE");
        index = Random.Range(0, bloodPart.Length);
        currentBlood = bloodPart[index];
        bloodinsta = Instantiate(currentBlood, SpawnDamage.transform.position, Quaternion.identity);

        if (playerBehaviour.chargeAttack == true)
        {
            anim.SetTrigger("Hit");
            mouthSounds.clip = hitSound;
            mouthSounds.Play();
        }
    }

    public void SeAcaboElHit()
    {
        anim.ResetTrigger("Hit");
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
