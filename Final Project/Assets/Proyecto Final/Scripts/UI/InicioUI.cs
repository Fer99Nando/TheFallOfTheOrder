using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InicioUI : MonoBehaviour
{
    public GameObject fade;

    public Animator anim;

    private float timeCounter;

    void Awake()
    {
        anim.SetBool("Fade", false);
    }

    void Update()
    {
        timeCounter += Time.deltaTime;
        InicioToMenuScene();
    }

    public void InicioToMenuScene()
    {

        if (Input.anyKeyDown)
        {
            StartCoroutine(Fading());
        }
    }

    IEnumerator Fading()
    {
        anim.SetBool("Fade", true);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Menu_Principal");
    }
}
