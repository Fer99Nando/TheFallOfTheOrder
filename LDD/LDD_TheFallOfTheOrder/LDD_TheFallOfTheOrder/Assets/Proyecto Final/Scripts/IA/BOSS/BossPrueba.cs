using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossPrueba : MonoBehaviour
{
    private enum EnemyState { Parado, Idle, Chase, Attack, Dead }
    [SerializeField] private EnemyState state;

    private NavMeshAgent agent;

    private Vector3 targetPosition;
    private GameObject player;

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

    public float idleTime;      // IDLE
    public float timeCounter = 0;  // Contador de tiempo
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
    }


    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case EnemyState.Parado:
                break;
            case EnemyState.Idle:
                IdleUpdate();
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

    }

    void LateUpdate()
    {
        distanceFromTarget = GetDistanceFromTarget();
    }

    #region AllUpdatesStates

    void IdleUpdate()
    {
        timeCounter += Time.deltaTime;

        if (timeCounter >= 5)
        {
            state = EnemyState.Chase;
            timeCounter = 0;

            return;
        }
    }

    void ChaseUpdate()
    {
        // animacion de giro hacia el personaje
        anim.SetBool("Chase", true);

        agent.SetDestination(player.transform.position);

        if (distanceFromTarget > attackRange)

        {
            agent.SetDestination(player.transform.position);
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
    }

    public void SetChase()
    {
        // Animacion de caminar


        agent.speed = chaseSpeed;   // La velocidad del enemigo pasa a ser igual que la de modo persecucion

        state = EnemyState.Chase;   // El estado pasa a ser persecucion
    }

    void SetAction()
    {
        // Sonidos de Ataque si los tiene
        agent.stoppingDistance = 1;
        state = EnemyState.Attack;
    }

    #endregion

    float GetDistanceFromTarget()       // Calcula la distancia con el player
    {
        return Vector3.Distance(player.transform.position, transform.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void BecomeBoss()
    {
        SetIdle();
    }

    public void BossPhase()
    {
        //anim.SetBool("PhaseTwo", true);
    }
}