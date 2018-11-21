using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour 
{
	public int startingHp;
	
	public int currentHp;

    private int damage;

	public Slider healthSlider;

	public Image damageImage;

	// Sonido muerte

	public float flashSpeed = 5f;

	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

	Animator anim;

	// Sonidos

	PlayerControl playerControl;

	bool isDead;
	bool damaged;

	void Awake()
	{
		anim = GetComponent<Animator>();
		playerControl = GetComponent<PlayerControl>();
		currentHp = startingHp;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (damaged)
		{
			damageImage.color = flashColour;
		}
		else
		{
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;
	}

	public void TakeDamage (int amount)
	{
		damaged = true;

        damage = amount;
		currentHp -= amount;

		healthSlider.value = currentHp;

		// Sonido asignado del jugador

		if(currentHp <= 0 && !isDead)
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
        if (other.CompareTag ("Damage"))
        {
            damage = 10;
        }
    }
}
