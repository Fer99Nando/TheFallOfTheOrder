using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour 
{
    private float maxVirus = 100;

    /*[Header("Material")]
    public Material mat1;
    public Material mat2;*/

    [Header("Life")]
	public float startingHp;
	public float startingV;
	
	[Header("Life InGame")]
	public float currentHp;
	public float currentV;

	[Header("Times")] 
	private float toxicTime;
	private float timeCount;

    [Header("Bars")]
    public Image healthSlider;
    public Image virusSlider;

    // Activamos el gameobject GameOver
    public GameObject gameOver;
    public GameObject personajes;

    // Sonido muerte

    [Header("Animation")]
	Animator anim;

	// Sonidos

	PlayerBehaviour playerControl;

	bool isDead;
	bool damaged;
	bool intoxicate = false;

	void Awake()
	{
		anim = GetComponent<Animator>();
		playerControl = GetComponent<PlayerBehaviour>();
		currentHp = startingHp;
        healthSlider.fillAmount = currentHp/startingHp;
		currentV = startingV;
        //virusSlider.value = startingV;
        virusSlider.fillAmount = 0;
        isDead = false;
    }
	
	// Update is called once per frame
	void Update () 
	{
        TakeDamage();

        if (currentHp > 100)
        {
            currentHp = startingHp;
        }
        if (currentV > 100)
        {
            currentV = maxVirus;
        }
        if (currentV < 0)
        {
            currentV = startingV;
        }

        if(intoxicate)
		{
			timeCount += Time.deltaTime;
			toxicTime += Time.deltaTime;
			Virus();
		}
    }

	void TakeDamage ()
	{
        healthSlider.fillAmount = currentHp / startingHp;
        if (currentHp <= 0 && !isDead)
        {
            Death();
        }
    }

	public void TakeVirus ()
	{
        float vDamage;
        vDamage = 10;
        currentV += vDamage;
        intoxicate = true;
        Debug.Log("Me sube el virusOOOOOOOOOOOOO");

        virusSlider.fillAmount = currentV / 100;

		// Sonido asignado del jugador

		if(currentHp <= 0 && !isDead)
		{
			return;
		}
	}

	void Virus()
	{
		if (currentHp > 0)
		{
			
			if (timeCount >= 3f)
			{
				Debug.Log("Me sube el Virus");
				currentV += 10;

                virusSlider.fillAmount = currentV / 100;
				
				timeCount = 0;
                return;
			}
			if (toxicTime >= 8)
			{
					intoxicate = false;
					toxicTime = 0;
			}
		}
	}

    #region POTIONS
    public void PotionHelath ()
    {
        currentHp += 20;
    }

    public void PotionAntidoto ()
    {
        currentV -= 20;
        virusSlider.fillAmount = currentV / 100;
    }

    public void PotionAllInOne ()
    {
        currentHp += 20;
        currentV -= 20;
        virusSlider.fillAmount = currentV / 100;
    }
    #endregion

    public void MaximusPower()
    {
        if (currentHp > 0)
        {
            Debug.Log("OJO QUE ME QUEDO SIN VIDA");
            currentHp -= Time.deltaTime;

            healthSlider.fillAmount = currentHp / startingHp;
        }
        else
        {
            Death();
        }
    }

   /* private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            transform.GetComponent<MeshRenderer>().material = mat2;
        }
        else transform.GetComponent<MeshRenderer>().material = mat1;
    }*/

    public void Death()
	{
		isDead = true;

        // Animacion de muerte;
        Destroy(personajes);
		playerControl.enabled = false;
        gameOver.SetActive(true);
        Cursor.visible = true;
    }


}
