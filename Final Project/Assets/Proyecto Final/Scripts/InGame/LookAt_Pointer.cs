using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt_Pointer : MonoBehaviour
{
    public Transform player;
    private float sensitivityX;
    private float mouseX;
    private float sensitivityY;
    private float mouseY;

    // Use this for initialization
    void Start ()
    {
        transform.position = player.position + new Vector3(0, 2f, 0) + transform.right * 0.5f;

        this.sensitivityX = 2f;
        this.sensitivityY = 1.5f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = player.position + new Vector3(0, 2f, 0) + transform.right * 0.5f;

        mouseX = Input.GetAxis("Mouse X") * sensitivityX;
        mouseY = Input.GetAxis("Mouse Y") * sensitivityY;
        Vector3 rotation = transform.eulerAngles;
        float rotationX;

        // Convertimos la rotacion [0-360] a [-90, 90] grados
        if (rotation.x > 90)
            rotationX = rotation.x - 360;
        else
            rotationX = rotation.x;

        // Rotamos sobre el eje X
        if ((mouseY < 0 && rotationX < 75) || (mouseY > 0 && rotationX > -60))
        {
            transform.Rotate(-mouseY, 0, 0);
        }

        // Rotamos sobre un eje vertical
        transform.RotateAround(transform.position, Vector3.up, this.mouseX);
    }
}
