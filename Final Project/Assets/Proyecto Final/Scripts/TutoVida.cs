using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutoVida : MonoBehaviour
{
    public GameObject General;

    private void Awake()
    {
        General.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            General.SetActive(true);
            Time.timeScale = 0f;
            Cursor.visible = true;
            Destroy(gameObject);
        }
    }
}
