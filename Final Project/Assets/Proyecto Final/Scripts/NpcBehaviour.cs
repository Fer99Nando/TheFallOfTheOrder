using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcBehaviour : MonoBehaviour
{
    private Animator anim;
	
	void Start ()
    {
        anim = GetComponent<Animator>();
	}

    public void HasBecomeInvisible()
    {
        this.gameObject.SetActive(false);
        anim.enabled = false;
    }

    public void HasBecomeVisible()
    {
        this.gameObject.SetActive(true);
        anim.enabled = true;
    }
}
