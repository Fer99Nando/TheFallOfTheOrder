using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFootsL : MonoBehaviour
{
    public AudioSource footStepLeft;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Grounded")
        {
            footStepLeft.Play();
        }
    }


}
