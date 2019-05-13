﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamage : MonoBehaviour
{
    public Vector3 boxSize;
    public Vector3 offset;
    private bool overlap;
    public int bonusStats;

    public void Box()
    {
        Collider[] cols = Physics.OverlapBox(transform.position + transform.right * offset.x + transform.up * offset.y + transform.forward * offset.z, boxSize / 2, transform.rotation);

        if (cols.Length > 0)
        {
            overlap = true;
            for (int i = 0; i < cols.Length; i++)
            {
                if(cols[i].gameObject.tag == "Enemy")
                {
                    EnemyHealth enemy = cols[i].GetComponent<EnemyHealth>();
                    enemy.TakeDamage(bonusStats);
                }

                if (cols[i].gameObject.tag == "Boss")
                {
                    BossHealth boss = cols[i].GetComponent<BossHealth>();
                    boss.TakeDamage(bonusStats);
                }
                Debug.Log(cols[i].name);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Matrix4x4 def = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(transform.position + transform.right * offset.x + transform.up * offset.y + transform.forward * offset.z, transform.rotation, boxSize);
        if (overlap) Gizmos.color = Color.cyan;
        else Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        Gizmos.matrix = def;
    }
}