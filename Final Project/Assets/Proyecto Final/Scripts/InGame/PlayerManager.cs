using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerManager : MonoBehaviour 
{

	int curHp;
	int curVirus;

	public int maxHp = 100;

	public Image healthBar;
	public Image virusBar;

    private float timeCount;
	private float toxicTime;

	private bool intoxicate;

    //Animator myAnim;

    void Start () 
	{
		//myAnim = GetComponent<Animator> ();
		
		curHp = maxHp;

		healthBar.fillAmount = curHp / maxHp;

		virusBar.fillAmount = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		timeCount += Time.deltaTime;

		if(timeCount >= 3 && intoxicate)
		{
			curVirus += 2;

			virusBar.fillAmount = curVirus / 500;

			timeCount = 0;
		}

		if (intoxicate)
		{
			toxicTime += Time.deltaTime;
			if(toxicTime >= 7)
			{
				intoxicate = false;
				toxicTime = 0;
			}
		}

	}

    public void SetDamage()
    {
        
        curHp -= 10;

        healthBar.fillAmount = curHp / maxHp;
    }

	private void OnTriggerEnter (Collider col)
	{

		if (col.CompareTag ("Enemy"))
		{
			intoxicate = true;

            SetDamage();
			//curHp -= col.GetComponent<EnemyManager>().damageValue;


			curVirus += 10;

			virusBar.fillAmount = curVirus / 500;

			if (curVirus >= 50)
			{
				//SONIDO 50
			}
			if (curVirus >= 100)
			{
				//SONIDO 100
			}

			if (healthBar.fillAmount <= 0)
			{
                //myAnim.SetBool("dead", true);

                GameOverManager.gameOverManager.CallGameOver();
			}

			toxicTime = 0;
		}
	}
}
