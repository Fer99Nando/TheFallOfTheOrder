using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingOptions : MonoBehaviour
{
    public GameObject fade;
    public GameObject fadeLayer;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        fadeLayer.SetActive(false);
    }

    public void ControlsOption()
    {
        fadeLayer.SetActive(true);
        anim.SetBool("Fade", true);
    }
}
