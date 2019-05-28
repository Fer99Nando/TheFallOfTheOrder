using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullingParticles : CullingGroupBase
{
    ParticleSystem[] ps;
    Light[] psLight;

    protected override void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("ParticleSystem");

        cullingObj = new Transform[objs.Length];
        ps = new ParticleSystem[objs.Length];
        psLight = new Light[objs.Length];

        for (int i = 0; i < objs.Length; i++)
        {
            cullingObj[i] = objs[i].transform;
            ps[i] = cullingObj[i].GetComponent<ParticleSystem>();
            psLight[i] = ps[i].GetComponentInChildren<Light>();
        }

        base.Start();

        group.SetBoundingDistances(distances);
        group.SetDistanceReferencePoint(reference);
    }

    /* private void Update()
    {
        for (int i = 0; i < cullingObj.Length; i++)
        {
            spheres[i].position = cullingObj[i].position;
        }
    }*/

    protected override void OnStateChanged(CullingGroupEvent sphere)
    {
        //Debug.Log("OnStateChanged: " + sphere.index + " IsVisible: " + sphere.isVisible);
        if (!sphere.isVisible)
        {
            ps[sphere.index].Stop();
            psLight[sphere.index].enabled = false;
        }
        else if (sphere.isVisible)
        {
            if (sphere.currentDistance == 1) // near to infinity
            {
                //Debug.Log("FAR");
                ps[sphere.index].Stop();
                psLight[sphere.index].enabled = false;
            }
            else
            {
                ps[sphere.index].Play();
                psLight[sphere.index].enabled = true;
            }
        }
    }
}
