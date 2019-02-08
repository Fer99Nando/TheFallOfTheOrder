using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    Vector3 rot = new Vector3(0, 180, 0);

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rot * Time.deltaTime);
    }
}
