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

	public LayerMask cameraMask;

	private float timeCount;

	public GameObject[] enemy;
	public float focusCamera; // Focus hacia un enemigo
	private float distanceFromTarget = Mathf.Infinity;     // Distancia del target que puede ser hasta infinito

	void Awake()
	{
		enemy = GameObject.FindGameObjectsWithTag("Enemy");
	}
    // Use this for initialization
    void Start () 
	{
		Cursor.visible = false;

		this.positionIncrease = this.lookAt.forward * this.distance; //Calcula el incremento de la posicion
		transform.position = this.lookAt.position - this.positionIncrease; //Creamos una posicion orbital
		transform.LookAt(this.lookAt); // la camara mira al lookAt (puntero)

	}
	
	// Update is called once per frame
	void Update () 
	{
		/*if (Input.GetButtonDown("Focus_Enemy"))
		{

		}*/

		this.distance = Mathf.Clamp(this.distance, 5f, 6f); // Limitacion de zoom in and zoom out

		this.fixedDist = FixDistance();
	}
	void LateUpdate()
	{
		//distanceFromTarget = GetDistanceFromTarget();

		this.positionIncrease = this.lookAt.forward * this.fixedDist;
		transform.position = Vector3.Lerp(transform.position, this.lookAt.position - this.positionIncrease, 5f * Time.deltaTime );

		transform.LookAt(this.lookAt);
	}
	//Corrige la distancia ante las colisiones u oclusiones de la camara
	float FixDistance()
	{
		RaycastHit hit; //Guarda la informacion de la colision

        /*Emite un raycast desde el lookAt a la camara colisionara con el primer collider que encuentre ( punto mas cercano al lookAt colisionara solo en el layer pasado por parametro */
        if (Physics.Raycast(this.lookAt.position, -this.lookAt.forward, out hit, this.distance, cameraMask))
        {
            Debug.DrawLine(this.lookAt.position, hit.point, Color.red);
            return hit.distance; //si colisiona se devuelve la distancia corregida
        }
        return hit.distance = 5; //Si no colisiona se devuelve la distancia original
 
	}

    /*float GetDistanceFromTarget()       // Calcula la distancia con el player
    {
        return Vector3.Distance(enemy.transform.position, transform.position);
    }*/
	    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, focusCamera);
	}
}
