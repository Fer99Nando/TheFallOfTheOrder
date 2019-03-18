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
	
	private GameObject closestEnemy;
	private float closestDist;

	private GameObject focusEnemy;
	private float timeCount;

	public GameObject player;
	private Transform playerPos;
	public GameObject[] enemy;
	public float focusCamera; // Focus hacia un enemigo
	private float distanceFromTarget = Mathf.Infinity;     // Distancia del target que puede ser hasta infinito

	private bool tab = false;

	void Awake()
	{
		//player = GameObject.FindGameObjectWithTag("Player");
		enemy = GameObject.FindGameObjectsWithTag("Enemy");
		playerPos = player.transform;
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
			if(tab)
			{
				tab = false;
			}else
			{
				FocusRange(playerPos.position, 10f);
			}
		}*/

		playerPos = player.transform;

		this.distance = Mathf.Clamp(this.distance, 5f, 6f); // Limitacion de zoom in and zoom out

		this.fixedDist = FixDistance();

		transform.LookAt(this.lookAt);

		/*if(tab)
		{
			transform.LookAt(closestEnemy.transform.position);
		}else
		{
			transform.LookAt(this.lookAt);
		}*/
	}
	void LateUpdate()
	{
		//distanceFromTarget = GetDistanceFromTarget();

		this.positionIncrease = this.lookAt.forward * this.fixedDist;
		transform.position = Vector3.Lerp(transform.position, this.lookAt.position - this.positionIncrease, 5f * Time.deltaTime );
		
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

	    /*public void FocusRange(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        int i = 0;
        while (i < hitColliders.Length)
        {
			tab = true;
			if(hitColliders[i].tag == "Enemy")
			{
				float distance = Vector3.Distance (playerPos.position, hitColliders[i].transform.position);

				if(i == 0){
					distance = Vector3.Distance (playerPos.position, hitColliders[i].transform.position);
					closestEnemy = hitColliders[i].gameObject;
				}

				if(distance < closestDist)
				{
					closestEnemy = hitColliders[i].gameObject;
					closestDist = distance;
				}
				Debug.Log(hitColliders[i].name);
			}
            i++;
        }
    }*/
	/*private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(playerPos.position, focusCamera);
	} */
	    
}
