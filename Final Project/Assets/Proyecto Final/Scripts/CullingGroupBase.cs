using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullingGroupBase : MonoBehaviour
{
    protected CullingGroup group;
    protected BoundingSphere[] spheres;

    public Transform[] cullingObj;
    public Transform reference;
    public float[] distances;

    protected virtual void Start ()
    {
        group = new CullingGroup();
        
        group.targetCamera = Camera.main;

        spheres = new BoundingSphere[100];

        int len = cullingObj.Length;
        if(len > 100) len = 100;

        for (int i = 0; i < len; i++)
        {
            spheres[i] = new BoundingSphere(cullingObj[i].position, 1.5f);
        }

        group.SetBoundingSpheres(spheres);
        group.SetBoundingSphereCount(len);

        //callback
        group.onStateChanged = OnStateChanged;
    }

    protected virtual void OnStateChanged(CullingGroupEvent sphere)
    {
    }

    private void OnDestroy()
    {
        group.Dispose();
        group = null;
    }
    protected virtual void OnDrawGizmos()
    {
        if(!Application.isPlaying) return;

        Gizmos.color = Color.red;

        int len = cullingObj.Length;
        for(int i = 0; i < len; i++)
        {
            Gizmos.DrawWireSphere(spheres[i].position, spheres[i].radius);
        }
    }
}
