using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraThirdPerson : MonoBehaviour 
{
	public Transform lookAt; //lookAt del personaje

	Vector3 positionIncrease; //Incremento de la posicion de la camara respecto al lookAt

	public float distance; //distancia original de la camara respecto al lookAt
	float fixedDist; //distancia corregida de la camara respecto al lookAt en caso de collision u oclusion

	float sensitivityWheel; //sensibilidad del zoom (Rueda del raton)

    public Transform cameraPos;

    // Use this for initialization
    void Start () 
	{
		this.positionIncrease = this.lookAt.forward * this.distance; //Calcula el incremento de la posicion
		transform.position = this.lookAt.position - this.positionIncrease; //Creamos una posicion orbital
		transform.LookAt(this.lookAt); // la camara mira al lookAt (puntero)

		this.sensitivityWheel = 3f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.distance += Input.GetAxis("Mouse ScrollWheel") * this.sensitivityWheel; //zoom
		this.distance = Mathf.Clamp(this.distance, 2f, 6f); // Limitacion de zoom in and zoom out

		this.fixedDist = fixDistance();
	}
	void LateUpdate()
	{
		this.positionIncrease = this.lookAt.forward * this.fixedDist;
		transform.position = Vector3.Lerp(transform.position, this.lookAt.position - this.positionIncrease, 0.3f * Time.deltaTime );
		transform.LookAt(this.lookAt);
	}
	//Corrige la distancia ante las colisiones u oclusiones de la camara
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
