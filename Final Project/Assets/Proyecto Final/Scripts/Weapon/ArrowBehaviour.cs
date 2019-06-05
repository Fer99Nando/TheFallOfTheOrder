using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    //public DistanceEnemy distanceEnemy;
    private PlayerHealth playerHealth;
    private GameObject player;

    public ParticleSystem trail;

    //public ParticleSystem trail;
    private Vector3 targetPosition;

    //bool arrowWall;

    //float arrowLife;

    void Start()
    {
        //arrowWall = false;

        player = GameObject.FindGameObjectWithTag("Player");

        if ((object)player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
            

            targetPosition = player.transform.position - transform.position;
            Quaternion newRotation = Quaternion.LookRotation(targetPosition);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 1);
        }

        trail.Play();
    }

    /*private void Update()
    {
        if(arrowWall)
        {
            arrowLife += Time.deltaTime;

            if(arrowLife > 3)
            {
                Destroy(this.gameObject);
            }
        }
    }*/

    private void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Player" || col.collider.tag == "PlayerWeapon")
        {
            Debug.Log("AAuuuuuux");
            playerHealth.Damage(3);
            Destroy(this.gameObject);
        }

        if (col.collider.tag == "Wall")
        {
            /*distanceEnemy = distanceEnemy.GetComponent<DistanceEnemy>();
            distanceEnemy.particleSpeed = 0;
            arrowWall = true;*/

            Destroy(this.gameObject);
        }
    }
}
