using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullingNPC : CullingGroupBase
{
    NpcBehaviour[] npcs;

    protected override void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Npc");

        cullingObj = new Transform[objs.Length];
        npcs = new NpcBehaviour[objs.Length];
        for(int i = 0; i < objs.Length; i++)
        {
            cullingObj[i] = objs[i].transform;
            npcs[i] = cullingObj[i].GetComponent<NpcBehaviour>();
        }

        base.Start();

        group.SetBoundingDistances(distances);
        group.SetDistanceReferencePoint(reference);
    }

    private void Update()
    {
        for(int i = 0; i < cullingObj.Length; i++)
        {
            spheres[i].position = cullingObj[i].position;
        }
    }

    protected override void OnStateChanged(CullingGroupEvent sphere)
    {
        if (sphere.hasBecomeInvisible)
        {
            
            npcs[sphere.index].HasBecomeInvisible();
        }
        if (sphere.hasBecomeVisible)
        {
            npcs[sphere.index].HasBecomeVisible();
        }
    }

}
