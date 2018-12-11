using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour 
{
	//public int damage;
    private int bonusStats;

    public GameObject gameOver;

    PlayerBehaviour playerControl;

    public Slider virusSlider;
    public Slider healthSlider;

    void Awake()
    {
        playerControl = GetComponent<PlayerBehaviour>();
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
        //if (virus.TakeVirus());
        if (virusSlider.value == 0)
        {
            Debug.Log("Base Daño");
            bonusStats = 5;
        }

        if (virusSlider.value > 0 && virusSlider.value < 25)
        {
            Debug.Log("10 Daño");
            bonusStats = 10;
        }

        if (virusSlider.value >= 25 && virusSlider.value < 50)
        {
            Debug.Log("15 Daño");
            bonusStats = 15;
        }
        
        if (virusSlider.value >= 50 && virusSlider.value < 75)
        {
            Debug.Log("20 Daño");
            bonusStats = 20;
        }

        if (virusSlider.value >= 75 && virusSlider.value < 100)
        {
            Debug.Log("25 Daño");
            bonusStats = 25;
        }

        if (virusSlider.value == 100)
        {
            Debug.Log("50 Daño");
            bonusStats = 50;

            if (healthSlider.value > 0)
            {
                healthSlider.value -= Time.deltaTime;
                return;
            }

            if (healthSlider.value <= 0)
            {
                healthSlider.value = 0;
                Death();
            }
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
