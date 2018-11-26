using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour 
{
	[Header("Life")]
	public int startingHp;
	public int startingV;
	
	[Header("Life InGame")]
	public int currentHp;
	public int currentV;

	[Header("Damage")]
    private int damage;
	private int vDamage;

	[Header("Times")] 
	private float toxicTime;
	private float timeCount;

	[Header("Bars")]
	public Slider healthSlider;
	public Slider virusSlider;

	public Image damageImage;

	// Sonido muerte

	public float flashSpeed = 5f;

	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

	[Header("Animation")]
	Animator anim;

	// Sonidos

	PlayerControl playerControl;

	bool isDead;
	bool damaged;
	bool intoxicate = false;


	void Awake()
	{
		anim = GetComponent<Animator>();
		playerControl = GetComponent<PlayerControl>();
		currentHp = startingHp;
		currentV = startingV;
		virusSlider.value = startingV;
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

		if(intoxicate)
		{
			timeCount += Time.deltaTime;
			toxicTime += Time.deltaTime;
			Virus();
		}
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

		public void TakeVirus (int vAmount)
	{
		
		intoxicate = true;

        vDamage = vAmount;
		currentV += vAmount;

		virusSlider.value = currentV;

		// Sonido asignado del jugador

		if(currentHp <= 0 && !isDead)
		{
			return;
		}
	}

    /*public void SetDamage()
    {
        if (state == EnemyState.Dead) return;   // Si el estado es muerto, sale de esta funcion

        if (playerHealth.currentHp > 0)
        {
            playerHealth.TakeDamage(damage);
            SetIdle();
            return;
        }

        if (life <= 0)
        {
            SetDead();
            return;
        }
    }*/

	void Virus()
	{
		if (currentHp > 0)
		{
			
			if (timeCount >= 3f)
			{
				Debug.Log("Me sube el Virus");
				currentV += 20;

				virusSlider.value = currentV;
				
				timeCount = 0;
			}

			toxicTime += Time.deltaTime;
			if (toxicTime >= 7)
			{
					intoxicate = false;
					toxicTime = 0;
			}
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
			Debug.Log("ENTRA EN EL TAG");
            damage = 10;
			vDamage = 20;
        }
    }
}
