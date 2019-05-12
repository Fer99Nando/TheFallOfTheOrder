using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaTuto : MonoBehaviour
{
    public GameObject panel;

    TutoVida tutoGeneral;
    public GameObject objVida;

    private void Start()
    {
        tutoGeneral = objVida.GetComponent<TutoVida>();
        panel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            tutoGeneral.timeOnV = true;
            panel.SetActive(true);
        }
    }
}
