using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoPots : MonoBehaviour
{
    public GameObject panelInfo;

    // Start is called before the first frame update
    void Start()
    {
        panelInfo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Entro");
            panelInfo.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        panelInfo.SetActive(false);
        Destroy(this.gameObject);
    }
}
