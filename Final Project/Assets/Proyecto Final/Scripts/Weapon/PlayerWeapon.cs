using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour 
{
    public int bonusStats;
    public int attackStats;

    public GameObject gameOver;

    PlayerHealth playerHealth;

    public Image virusSlider;
    public Image healthSlider;
    public ParticleSystem virusEffect;
    BoxCollider boxCol;

    void Start()
    {
        attackStats = 10;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        boxCol = GetComponent<BoxCollider>();
        boxCol.enabled = false;
    }
    void Update()
    {
        bonusStats = attackStats;
        DamageVirus();
    }

    void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
		{
            Debug.Log("enemy atravesado");
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
			enemy.TakeDamage (bonusStats);
            boxCol.enabled = false;
        }

        if (other.tag == "Boss")
        {
            Debug.Log("BOSS atravesado");
            BossHealth boss = other.GetComponent<BossHealth>();
            boss.currentHp -= bonusStats;
            boxCol.enabled = false;
        }
    }

    public void DamageVirus()
    {
        Debug.Log("Sube Daño");

        if (virusSlider.fillAmount == 0)
        {
            //Debug.Log("Base Daño");
            //attackStats = 5;
        }

        else if (virusSlider.fillAmount > 0 && virusSlider.fillAmount < 0.25f)
        {
            //Debug.Log("10 Daño");
            attackStats = 15;
        }

        if (virusSlider.fillAmount >= 0.25f && virusSlider.fillAmount < 0.5f)
        {
            //Debug.Log("15 Daño");
            attackStats = 20;
        }
        
        if (virusSlider.fillAmount >= 0.5f && virusSlider.fillAmount < 0.75f)
        {
           // Debug.Log("20 Daño");
            attackStats = 30;
        }

        if (virusSlider.fillAmount >= 0.75f && virusSlider.fillAmount < 1f)
        {
            //Debug.Log("25 Daño");
            attackStats = 40;
        }

        if (virusSlider.fillAmount >= 1)
        {
            //Debug.Log("50 Daño");
            attackStats = 50;
            virusEffect.Play();

            playerHealth.MaximusPower();
        } else virusEffect.Stop();
    }

    public void BoxEnabled()
    {
        boxCol.enabled = true;
    }

    public void BoxDisabled()
    {
        boxCol.enabled = false;
    }

    public void Death()
    {
        gameOver.SetActive(true);
        Cursor.visible = true;
        Destroy(gameObject);
    }
}
