using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerManager : MonoBehaviour 
{
	float curHp;
	float curVirus;
	public float maxHp = 100f;
	public float maxVirus = 100f;

	public Image healthBar;
	public Image virusBar;

	private float counterVirus;

	//Animator myAnim;

	// Use this for initialization
	void Start () 
	{
		counterVirus++;

		//myAnim = GetComponent<Animator> ();
		
		curHp = maxHp;

		healthBar.fillAmount = curHp / maxHp;

		virusBar.fillAmount = curVirus / maxVirus;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	void LateUpdate ()
	{
		if (counterVirus > 24f)
		{
			curVirus += 2f;

			virusBar.fillAmount = curVirus / maxVirus;
		}
		if (counterVirus > 26)
		{
			counterVirus = 0;
		}
	}
	private void OnTriggerEnter (Collider col)
	{
		if (col.CompareTag ("Enemy"))
		{
			//curHp -= col.GetComponent<EnemyManager>().damageValue;
			curHp -= 10f;

			healthBar.fillAmount = curHp / maxHp;

			if (curHp <= 0)
			{
			//myAnim.SetBool("dead", true);
			}
		}
	}
}
