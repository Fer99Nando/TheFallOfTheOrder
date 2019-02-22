using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFoots : MonoBehaviour
{
    public AudioSource footStepRight;
    public ParticleSystem dust;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Grounded")
        {
            Debug.Log("TocoSuelo");
            footStepRight.Play();
            dust.Emit(1);
        }
    }


}
