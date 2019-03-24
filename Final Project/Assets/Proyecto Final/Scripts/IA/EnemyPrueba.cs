using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPrueba : MonoBehaviour
{

    private enum EnemyState { Idle, Patrol, Chase, Attack, Dead }
    [SerializeField] private EnemyState state;

    private NavMeshAgent agent;

    private Vector3 targetPosition;
    private GameObject player;
    EnemyHealth enemyDeath;

    [Header("Paths")]

    public Transform[] points;
    public int pathIndex = 0;
    public float chaseRange;        // Rango de Persecucion
    public float attackRange;       // Rango de Ataque
    [SerializeField] private float distanceFromTarget = Mathf.Infinity;     // Distancia del target que puede ser hasta infinito

    [Header("Speeds")]

    public float chaseSpeed;        // Velocidad de Persecucion
    public float patrolSpeed;       // Velocidad  mientras Patrulla
    public float rotationSpeed;     // Velocidad  rotation hacia el player

    [Header("Timers")]

    public float idleTime;      // IDLE
    private float timeCounter = 0;  // Contador de tiempo
    private float timeToPatrol = 0; // Contador para pasar a patrol desde chase

    public float coolDownAttack = 0;   // Enfriamineto despues de atacar

    [Header("Stats")]

    //private bool canAttack = false;     // El ataque del enemigo desactivado

    [Header("Animation")]

    public Animator anim;           // Para poder poner Animaciones

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        anim = GetComponent<Animator>();        // Llamamos a las animaciones

        player = GameObject.FindGameObjectWithTag("Player");

        enemyDeath = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case EnemyState.Idle:
                IdleUpdate();
                break;
            case EnemyState.Patrol:
                PatrolUpdate();
                break;
            case EnemyState.Chase:
                ChaseUpdate();
                break;
            case EnemyState.Attack:
                ActionUpdate();
                break;
            default:
                break;
        }

        if (enemyDeath.isDead == false)
        {
            targetPosition = player.transform.position - transform.position;
            targetPosition.y = 0;
            Quaternion newRotation = Quaternion.LookRotation(targetPosition);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotationSpeed * 0.1f);
        }
    }

    void LateUpdate()
    {
        distanceFromTarget = GetDistanceFromTarget();
    }

    #region AllUpdatesStates

    void IdleUpdate()
    {
        // El personaje este quieto
        if (timeCounter >= idleTime)
        {
            SetPatrol();
        }
        else timeCounter += Time.deltaTime;
    }

    void PatrolUpdate()
    {
        anim.SetBool("Walk", true);

        if (distanceFromTarget < chaseRange)
        {
            anim.SetBool("Walk", false);
            SetChase();
            return;
        }

        if (agent.remainingDistance <= agent.stoppingDistance)  // Por si acaso Que explique alex
        {
            pathIndex++;

            if (pathIndex >= points.Length)
            {
                pathIndex = 0;
            }

            SetIdle();  // Si queremos que se pare cuando llegue a un punto
        }

        // Aqui va el rugido de los monstruos cada x tiempo
        // if (timeCounter >= roarTime)
    }

    void ChaseUpdate()
    {
        // animacion de giro hacia el personaje
        anim.SetBool("Chase", true);

        agent.SetDestination(player.transform.position);

        if (distanceFromTarget > chaseRange)
        {
            anim.SetBool("Chase", true);
            timeToPatrol += Time.deltaTime;
            if (timeToPatrol >= 3)
            {
                SetPatrol();
                timeToPatrol = 0;
                return;
            }
        }
        else
        {
            if (distanceFromTarget > attackRange)

            {
                agent.SetDestination(player.transform.position);
            }
        }

        if (distanceFromTarget < attackRange)
        {
            anim.SetBool("Chase", false);
            SetAction();
            return;
        }
    }

    void ActionUpdate()
    {
        Debug.Log("CASI DAÑO");
        agent.SetDestination(player.transform.position);

        if (distanceFromTarget < attackRange)
        {
            chaseSpeed = 0;
            agent.stoppingDistance = 2;
            Debug.Log("ATTACK");
            agent.isStopped = true;
            anim.SetBool("Action", true);
            idleTime = coolDownAttack; // Esto es si quiero que tenga un time para quese  enfrie y poderle atacar

            return;

        }
        else
        {
            Debug.Log("Salio Del Range");
            anim.SetBool("Action", false);
            agent.isStopped = false;
            SetChase();
            return;
        }
    }

    #endregion

    #region Sets

    void SetIdle()
    {
        state = EnemyState.Idle;

        timeCounter = 0;
    }

    void SetPatrol()
    {
        agent.isStopped = false;
        //agent.Resume();

        // Animacion de caminar

        agent.speed = patrolSpeed;   // La velocidad del enemigo pasa a ser igual que la de modo Patrol

        state = EnemyState.Patrol;   // El estado pasa a ser Patrol

        agent.SetDestination(points[pathIndex].position);

        timeCounter = 0;
    }

    void SetChase()
    {
        // Animacion de caminar


        agent.speed = chaseSpeed;   // La velocidad del enemigo pasa a ser igual que la de modo persecucion

        state = EnemyState.Chase;   // El estado pasa a ser persecucion
    }

    void SetAction()
    {
        // Sonidos de Ataque si los tiene
        agent.stoppingDistance = 2;
        state = EnemyState.Attack;
    }

    #endregion

    float GetDistanceFromTarget()       // Calcula la distancia con el player
    {
        return Vector3.Distance(player.transform.position, transform.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}