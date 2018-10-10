using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private const float Y_ANGLE_MIN = -30.0f;
    private const float Y_ANGLE_MAX = 30.0f;

    public Transform lookAt;
    public Transform camTransform;
    public float distance = 10.0f;

    private float currentX = 0.0f;
    private float currentY = 45.0f;
    private float sensitivityX = 3.0f;
    private float sensitivityY = 3.0f;

    Vector3 positionIncrease;

    float fixedDist; //distancia corregida de la camara respecto al lookAt en caso de collision u oclusion

    private void Start()
    {
        this.positionIncrease = this.lookAt.forward * this.distance;
        camTransform = transform;
    }

    private void Update()
    {
        currentX += Input.GetAxis("Mouse X");
        currentY += Input.GetAxis("Mouse Y");

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

        this.fixedDist = fixDistance();
    }

    private void LateUpdate()
    {
        this.positionIncrease = this.lookAt.forward * this.fixedDist;

        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * dir;
        transform.LookAt(this.lookAt);
    }

    float fixDistance()
    {
        RaycastHit hit; //Guarda la informacion de la colision
        LayerMask layerMask = 1 << 8; //Asignamos el layer 8 que es el del player
        layerMask = ~layerMask; //invierte el layer para que colisione con cualquier layer excepto este

        /*Emite un raycast desde el lookAt a la camara colisionara conel primer collider que encuentre ( punto mas cercano al lookAt colisionara solo en el layer pasado por parametro */
        if (Physics.Raycast(this.lookAt.position, -this.lookAt.forward, out hit, this.distance, layerMask))
        {
            Debug.DrawLine(this.lookAt.position, hit.point, Color.red);
            return hit.distance; //si colisiona se devuelve la distancia corregida
        }
        return hit.distance; //Si no colisiona se devuelve la distancia original

    }
}
