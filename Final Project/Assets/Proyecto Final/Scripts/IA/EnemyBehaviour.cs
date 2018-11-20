using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
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

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Use this for initialization
    void Start ()
    {
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        distanceFromTarget = GetDistanceFromTarget();

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
            case EnemyState.Dead:
                //DeadUpdate();
                break;
            default:
                break;
        }

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
        distanceFromTarget = GetDistanceFromTarget();
        if (distanceFromTarget < chaseRange)
        {
            
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
        timeCounter = 0;

        Debug.Log("TE PERSIGO");

        agent.SetDestination(targetTransform.position);

        if (distanceFromTarget > chaseRange)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= 3)
            {
                SetPatrol();
                timeCounter = 0;
                Debug.Log("timeCounter %d");
                return;
            }
        }
        else SetChase();

        if (distanceFromTarget < attackRange)
        {
            SetAction();
            return;
        }
    }

    void ActionUpdate()
    {
        agent.SetDestination(targetTransform.position); 

        if (canAttack)
        {
            agent.isStopped = true;
            //agent.Stop(); // 5.5 // agent.isStopped = true; // 5.6 PREGUNTAR A ALEX

            // Recibir o hacer daño del player?
            targetTransform.GetComponent<PlayerManager>().SetDamage(); // preguntar esto Alex

            idleTime = coolDownAttack; // Esto es si quiero que tenga un time para quese  enfrie y poderle atacar

            SetIdle();
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
        // Animacion de atacar

        // Sonidos de Ataque si los tiene

        state = EnemyState.Attack;
    }

    void SetDead()
    {
        // Animacion de muerte

        agent.isStopped = true;
        state = EnemyState.Dead;

        // Sonidos de muerte si los tiene
        // Hacer un if de si la animacion a terminado llamar al destroy
        
    }

    #endregion

    #region PublicFunctions

    public void SetDamage(int hit)
    {
        if (state == EnemyState.Dead) return;   // Si el estado es muerto, sale de esta funcion

        life -= hit;

        if(life <= 0)
        {
            SetDead();
            return;
        }
    }

    #endregion

    float GetDistanceFromTarget()       // Calcula la distancia con el player
    {
        return Vector3.Distance(targetTransform.position, transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canAttack = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canAttack = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void DesactivateEnemy()
    {
        this.gameObject.SetActive(false);
    }

    public void OnDestroy()
    {
        Destroy(this.gameObject);
    }
}
