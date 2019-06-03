using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicaInicial : MonoBehaviour
{
    public GameObject info;
    public GameObject infoP;
    public float timeToDestroy;
    public PlayerBehaviour playerBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        playerBehaviour = playerBehaviour.GetComponent<PlayerBehaviour>();
        info.SetActive(false);
        infoP.SetActive(true);
        playerBehaviour.canMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeToDestroy += Time.deltaTime;

        if (timeToDestroy >= 19 || Input.anyKeyDown)
        {
            playerBehaviour.canMove = true;
            info.SetActive(true);
            Destroy(gameObject);
            Destroy(infoP);
        }
    }
}
