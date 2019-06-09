using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicaInicial : MonoBehaviour
{
    public bool cineOn;

    public GameObject info;
    public GameObject infoP;
    public float timeToDestroy;
    public PlayerBehaviour playerBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        cineOn = true;

        playerBehaviour = playerBehaviour.GetComponent<PlayerBehaviour>();
        info.SetActive(false);
        infoP.SetActive(true);
        playerBehaviour.canMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeToDestroy += Time.deltaTime;

        if (timeToDestroy >= 15 || Input.anyKeyDown)
        {
            cineOn = false;

            playerBehaviour.canMove = true;
            info.SetActive(true);
            Destroy(gameObject);
            Destroy(infoP);
        }
    }
}
