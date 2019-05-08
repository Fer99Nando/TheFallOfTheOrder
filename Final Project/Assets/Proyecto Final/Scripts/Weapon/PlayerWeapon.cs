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
    public AreaDamage areaDamage;
    private float maxVirus;

    void Start()
    {
        attackStats = 10;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        maxVirus = 100;
    }
    void Update()
    {        
        DamageVirus();
        bonusStats = attackStats;
    }

    public void Attack()
    {
        areaDamage.bonusStats = bonusStats;
        areaDamage.Box();
    }
    /*
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
    */
    public void DamageVirus()
    {        
        if (playerHealth.currentV >= maxVirus)
        {
            attackStats = 50;
        }
        else
        {
            if (playerHealth.currentV >= maxVirus * 0.75f) attackStats = 40;
            else if (playerHealth.currentV >= maxVirus / 2) attackStats = 30;
            else if (playerHealth.currentV >= maxVirus / 4) attackStats = 20;
            else if (playerHealth.currentV > 0) attackStats = 15;
            else attackStats = 10;
        }
    }

    public void Death()
    {
        gameOver.SetActive(true);
        Cursor.visible = true;
        Destroy(gameObject);
    }
}
