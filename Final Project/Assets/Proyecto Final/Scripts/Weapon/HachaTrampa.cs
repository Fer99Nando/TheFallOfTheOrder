using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HachaTrampa : MonoBehaviour
{
    public Transform trampita;
    public Transform trampita1;
    public Transform trampita2;
    public Transform trampita3;
    public Transform trampita4;
    public Transform trampita5;
    public Transform trampita6;

    private void Start()
    {
        TrampitaLoop();
        Trampita2Loop();
        Trampita3Loop();
        Trampita1Loop();
        Trampita4Loop();
        Trampita5Loop();
        Trampita6Loop();
    }

    void TrampitaLoop()
    {
        trampita.DORotate(new Vector3(0, 0, 135), 3, RotateMode.LocalAxisAdd).SetEase(Ease.OutCubic).OnComplete(TrampitaLoop2);
    }

    void TrampitaLoop2()
    {
        trampita.DORotate(new Vector3(0, 0, -135), 3, RotateMode.LocalAxisAdd).SetEase(Ease.OutCubic).OnComplete(TrampitaLoop);
    }


    void Trampita2Loop()
    {
        trampita1.DORotate(new Vector3(0, 0, -135), 3.5f, RotateMode.LocalAxisAdd).SetEase(Ease.OutQuad).OnComplete(Trampita2Loop2);
    }

    void Trampita2Loop2()
    {
        trampita1.DORotate(new Vector3(0, 0, 135), 3.5f, RotateMode.LocalAxisAdd).SetEase(Ease.OutQuad).OnComplete(Trampita2Loop);
    }


    void Trampita3Loop()
    {
        trampita2.DORotate(new Vector3(0, 0, 135), 4.5f, RotateMode.LocalAxisAdd).SetEase(Ease.OutCubic).OnComplete(Trampita3Loop2);
    }

    void Trampita3Loop2()
    {
        trampita2.DORotate(new Vector3(0, 0, -135), 4.5f, RotateMode.LocalAxisAdd).SetEase(Ease.OutCubic).OnComplete(Trampita3Loop);
    }


    void Trampita1Loop()
    {
        trampita3.DORotate(new Vector3(0, 0, -135), 4, RotateMode.LocalAxisAdd).SetEase(Ease.OutCirc).OnComplete(Trampita1Loop2);
    }

    void Trampita1Loop2()
    {
        trampita3.DORotate(new Vector3(0, 0, 135), 4, RotateMode.LocalAxisAdd).SetEase(Ease.OutCirc).OnComplete(Trampita1Loop);
    }


    void Trampita4Loop()
    {
        trampita4.DORotate(new Vector3(0, 0, 135), 3, RotateMode.LocalAxisAdd).SetEase(Ease.OutCubic).OnComplete(Trampita4Loop2);
    }

    void Trampita4Loop2()
    {
        trampita4.DORotate(new Vector3(0, 0, -135), 3, RotateMode.LocalAxisAdd).SetEase(Ease.OutCubic).OnComplete(Trampita4Loop);
    }


    void Trampita5Loop()
    {
        trampita5.DORotate(new Vector3(0, 0, 135), 4.5f, RotateMode.LocalAxisAdd).SetEase(Ease.OutCubic).OnComplete(Trampita5Loop2);
    }

    void Trampita5Loop2()
    {
        trampita5.DORotate(new Vector3(0, 0, -135), 4.5f, RotateMode.LocalAxisAdd).SetEase(Ease.OutCubic).OnComplete(Trampita5Loop);
    }


    void Trampita6Loop()
    {
        trampita6.DORotate(new Vector3(0, 0, -135), 3.5f, RotateMode.LocalAxisAdd).SetEase(Ease.OutQuad).OnComplete(Trampita6Loop2);
    }

    void Trampita6Loop2()
    {
        trampita6.DORotate(new Vector3(0, 0, 135), 3.5f, RotateMode.LocalAxisAdd).SetEase(Ease.OutQuad).OnComplete(Trampita6Loop);
    }
}
