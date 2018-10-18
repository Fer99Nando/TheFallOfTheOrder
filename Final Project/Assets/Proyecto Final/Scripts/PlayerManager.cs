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

	private float counterVirus;

	//Animator myAnim;

	// Use this for initialization
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
		
	}
	void LateUpdate ()
	{

	}
	private void OnTriggerEnter (Collider col)
	{
		if (col.CompareTag ("Enemy"))
		{
			//curHp -= col.GetComponent<EnemyManager>().damageValue;
			curHp -= 10f;

			healthBar.fillAmount = curHp / maxHp;

			curVirus += 10f;

			virusBar.fillAmount = curVirus / 500;

			if (curVirus >= 50)
			{
				//SONIDO 50
			}
			if (curVirus >= 100)
			{
				//SONIDO 100
			}

			if (curHp <= 0)
			{
				//myAnim.SetBool("dead", true);
			}
		}
	}
}
