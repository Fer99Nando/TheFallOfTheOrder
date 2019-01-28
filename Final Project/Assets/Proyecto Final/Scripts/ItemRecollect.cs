using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemRecollect : MonoBehaviour
{
    public GameObject lifePot;
    // Start is called before the first frame update
    void Start()
    {
        lifePot.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("Aaa, me tocaste");

            if (lifePot == false)
            {
                lifePot.SetActive(true);
            }

            Destroy(gameObject);
        }
    }
}
