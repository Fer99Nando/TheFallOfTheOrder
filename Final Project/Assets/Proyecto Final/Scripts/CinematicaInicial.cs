using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicaInicial : MonoBehaviour
{
    public GameObject info;
    public float timeToDestroy;
    // Start is called before the first frame update
    void Start()
    {
        //info.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timeToDestroy += Time.deltaTime;

        if (timeToDestroy >= 21)
        {
            info.SetActive(true);
            Destroy(gameObject);
        }

        
    }
}
