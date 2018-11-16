using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{

    private enum EnemyState { Idle, Patrol, Chase, Attack, Dead }
    [SerializeField] private EnemyState state;

    private NavMeshAgent agent;

    [SerializeField] private Transform targetTransform;

    [Header("Paths")]

    public Transform[] points;
    public int pathIndex = 0;

    public float chaseRange;        // Rango de Persecucion
    public float attackRange;       // Rango de Ataque
    [SerializeField] private float distanceFromTarget = Mathf.Infinity;     // Distancia del target que puede ser hasta infinito

    [Header("Speeds")]

    public float chaseSpeed;        // Velocidad de Persecucion
    public float patrolSpeed;       // Velocidad  mientras Patrulla

    [Header("Timers")]

    public float idleTime = 1;      // IDLE
    private float timeCounter = 0;  // Contador de tiempo

    public float coolDownAttack = 1f;   // Enfriamineto despues de atacar

    [Header("Stats")]

    private bool canAttack = false;     // El ataque del enemigo desactivado

    [Header("Properties")]

    public int hitDamage;           // Daño recibido
    public int life = 100;          // Vida del Enemy

    [Header("Animation")]

    public Animator anim;           // Para poder poner Animaciones

    // Use this for initialization
    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();

        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        /* poner aqui los case con un switch para que en cada caso
        vuelva a repetir el proceso y cambiar de case */

    }

    #region AllUpdatesStates

    void IdleUdate()
    {
        // El personaje este quieto
        if (timeCounter >= idleTime)
        {
            // Pasar al Patrol
        }
        else timeCounter += Time.deltaTime;
    }

    void PatrolUpdate()
    {
        if (distanceFromTarget < chaseRange)
        {
            // Pasar al chase
            return;
        }

        if (agent.remainingDistance <= agent.stoppingDistance) // Que Alex me explique esto
        {
            pathIndex++;

            if (pathIndex >= points.Length)
            {
                pathIndex = 0;
            }
        }
        // Si queremos que se pare cuando llegue a un punto
        // SetIdle();

        // Aqui va el rugido de los monstruos cada x tiempo
        // if (timeCounter >= roarTime)

    }

    void ChaseUpdate()
    {
        agent.SetDestination(targetTransform.position);

        if (distanceFromTarget > chaseRange)
        {
            // Pasa al Patrol
            return;
        }

        if (distanceFromTarget > attackRange)
        {
            // Pasa a Action
            return;
        }
    }

    void ActionUpdate()
    {
        agent.SetDestination(targetTransform.position);

        if (canAttack)
        {
            agent.Stop(); // 5.5 // agent.isStopped = true; // 5.6 PREGUNTAR A ALEX
            // Recibir daño del player
            // targetTransform.GetComponent<PlayerManager> SetDamage(hitDamage);
            idleTime = coolDownAttack; // Esto es si quiero que tenga un time para quese  enfrie y poderle atacar
            // Pasar a Idle
            return;
        }
    }

    void DeadUpdate()
    {
        // Quieto
        // Animacion de la muerte
    }
    #endregion

    #region Sets
    #endregion

    #region PublicFunctions
    #endregion
}
