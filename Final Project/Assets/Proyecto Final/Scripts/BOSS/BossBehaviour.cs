using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossBehaviour : MonoBehaviour
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
    private float timeToPatrol = 0; // Contador para pasar a patrol desde chase

    public float coolDownAttack = 0;   // Enfriamineto despues de atacar

    [Header("Stats")]

    private bool canAttack = false;     // El ataque del enemigo desactivado

    [Header("Properties")]

    public int life = 100;          // Vida del Enemy

    [Header("Animation")]

    public Animator anim;           // Para poder poner Animaciones

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        anim = GetComponent<Animator>();        // Llamamos a las animaciones

        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

	// Update is called once per frame
	void Update ()
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
                Debug.Log("PUPA");
                ActionUpdate();
                break;
            /*case EnemyState.Dead:
                //DeadUpdate();
                break;*/
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
        // El personaje este quieto
        if (timeCounter >= idleTime)
        {
            SetPatrol();
        }
        else timeCounter += Time.deltaTime;
    }

    void PatrolUpdate()
    {
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
        Debug.Log("Te Persigo");
        // animacion de giro hacia el personaje
        anim.SetBool("Chase", true);

        agent.SetDestination(targetTransform.position);

        if (distanceFromTarget > chaseRange)
        {
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
                agent.SetDestination(targetTransform.position);
            }
        }

        if (distanceFromTarget < attackRange)
        {
            SetAction();
            return;
        }
    }

    void ActionUpdate()
    {
        Debug.Log("CASI DAÑO");
        agent.SetDestination(targetTransform.position); 

        if (distanceFromTarget < attackRange)
        {
            
            Debug.Log("ATTACK");
            agent.isStopped = true;
            anim.SetBool("Action", true);
            //SetDamage();
            return;
            //agent.Stop(); // 5.5 // agent.isStopped = true; // 5.6 PREGUNTAR A ALEX

            // Recibir o hacer daño del player?
            //targetTransform.GetComponent<PlayerManager>().SetDamage(); // preguntar esto Alex

            //idleTime = coolDownAttack; // Esto es si quiero que tenga un time para quese  enfrie y poderle atacar
            
            //SetIdle();
        }
        else
        {
            Debug.Log("Salio Del Range");
            anim.SetBool("Action", false);
            SetChase();
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

    #endregion
    
    float GetDistanceFromTarget()       // Calcula la distancia con el player
    {
        return Vector3.Distance(targetTransform.position, transform.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void OnDestroy()
    {
        Destroy(this.gameObject);
    }
}

    /*void SetDead()
    {
        // Animacion de muerte

        agent.isStopped = true;
        state = EnemyState.Dead;

        // Sonidos de muerte si los tiene
        // Hacer un if de si la animacion a terminado llamar al destroy
        
    }*/

   /* #region PublicFunctions

    public void SetDamage()
    {
        if (state == EnemyState.Dead) return;   // Si el estado es muerto, sale de esta funcion

        if (playerHealth.currentHp > 0)
        {
            playerHealth.TakeDamage (damage);
                SetIdle();
                return;
        }

        if(life <= 0)
        {
            SetDead();
            return;
        }
    }

    #endregion*/

