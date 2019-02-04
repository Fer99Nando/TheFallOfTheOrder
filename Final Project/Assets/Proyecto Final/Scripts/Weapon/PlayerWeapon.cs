using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour 
{
    private int bonusStats;

    public GameObject gameOver;

    PlayerHealth playerHealth;

    public Image virusSlider;
    public Image healthSlider;

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
    void Update()
    {
        DamageVirus();
    }

    void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
		{
            Debug.Log("enemy atravesado");
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
			enemy.TakeDamage (bonusStats);
        }

        if (other.tag == "Boss")
        {
            BossHealth boss = other.GetComponent<BossHealth>();
            boss.TakeDamage(bonusStats);
        }
    }

    public void DamageVirus()
    {
        Debug.Log("Sube Daño");

        if (virusSlider.fillAmount == 0)
        {
            Debug.Log("Base Daño");
            bonusStats = 5;
        }

        else if (virusSlider.fillAmount > 0 && virusSlider.fillAmount < 0.25f)
        {
            Debug.Log("10 Daño");
            bonusStats = 10;
        }

        if (virusSlider.fillAmount >= 0.25f && virusSlider.fillAmount < 0.5f)
        {
            Debug.Log("15 Daño");
            bonusStats = 15;
        }
        
        if (virusSlider.fillAmount >= 0.5f && virusSlider.fillAmount < 0.75f)
        {
            Debug.Log("20 Daño");
            bonusStats = 20;
        }

        if (virusSlider.fillAmount >= 0.75f && virusSlider.fillAmount < 1f)
        {
            Debug.Log("25 Daño");
            bonusStats = 25;
        }

        if (virusSlider.fillAmount >= 1)
        {
            Debug.Log("50 Daño");
            bonusStats = 50;

            playerHealth.MaximusPower();
        }
    }

    public void Death()
    {
    // Animacion de muerte;
        gameOver.SetActive(true);
        Cursor.visible = true;
        Destroy(gameObject);
        //playerControl.enabled = false;
        
        
    }
}
