using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerManager : MonoBehaviour 
{

	float curHp;
	float curVirus;

	public float maxHp = 100f;

	public Image healthBar;
	public Image virusBar;

    private float timeCount;
	private float toxicTime;

	private bool intoxicate;

    EnemyHealth enemyHealth;

    //Animator myAnim;

    void Start () 
	{
        //myAnim = GetComponent<Animator> ();

        HealthBar();
	}
	
	// Update is called once per frame
	void Update () 
	{
        Virus();
	}

    #region Barras

    public void HealthBar()
    {
        curHp = maxHp;

        healthBar.fillAmount = curHp / maxHp;
        
        virusBar.fillAmount = 0;
    }

    public void Virus()
    {
        timeCount += Time.deltaTime;

        if (timeCount >= 3 && intoxicate)
        {
            curVirus += 2;

            virusBar.fillAmount = curVirus / 500;
            
            timeCount = 0;
        }

        if (intoxicate)
        {
            toxicTime += Time.deltaTime;
            if (toxicTime >= 7)
            {
                intoxicate = false;
                toxicTime = 0;
            }
        }
    }

    #endregion
    private void OnTriggerEnter (Collider col)
	{

		if (col.CompareTag ("Damage"))
		{
            Debug.Log("PUM DAÑO");
            
			intoxicate = true;

        curHp -= 10f;

        healthBar.fillAmount = curHp / maxHp;

			// curHp -= col.GetComponent<EnemyBehaviour>().damageValue;

			curVirus += 10f;

			virusBar.fillAmount = curVirus / 500;

			if (healthBar.fillAmount <= 0)
			{
                //myAnim.SetBool("dead", true);

                GameOverManager.gameOverManager.CallGameOver();
			}

			toxicTime = 0;
		}
	}
}
