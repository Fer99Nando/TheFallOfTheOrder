using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    private enum EnemyState { Idle, Patrol, Chase, Attack, Stun, Dead }
    [SerializeField] private EnemyState state;

    private NavMeshAgent agent;

    [SerializeField] private Transform targetTransform;

    [Header("Paths")]

    public Transform[] points;
    public int pathIndex = 0;

    [Header("Distances")]

    public float chaseRange;
    public float attackRange;
    [SerializeField] private float distanceFromTarget = Mathf.Infinity;

    [Header("Speeds")]

    public float chaseSpeed;
    public float patrolSpeed;

    [Header("Timers")]

    public float idleTime = 1;
    private float timeCounter = 0;

    public float coolDownAttack = 1f;
    public float stunTime = 1;
    private float moanTime = 0;

    [Header("Stats")]

    private bool canAttack = false;

    [Header("Properties")]

    public int hitDamage;
    public int life = 100;

    [Header("Audio")]

    public AudioSource zombieMoan;
    public AudioSource zombieReceiveDamage;
    public AudioSource zombieAttack;
    public AudioSource zombieDie;

    [Header("Animation")]

    public Animator anim;

	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();

        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;

        moanTime = Random.Range(3.5f, 10.0f);

        SetIdle();
	}
	
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
                AttackUpdate();
                break;
            case EnemyState.Stun:
                StunUpdate();
                break;
            case EnemyState.Dead:
                //DeadUpdate();
                break;
            default:
                break;
        }
	}

    #region Updates

    void IdleUpdate()
    {
        if (timeCounter >= idleTime)
        {
            SetPatrol();
        }
        else timeCounter += Time.deltaTime;
    }

    void PatrolUpdate()
    {
        if(distanceFromTarget < chaseRange)
        {
            SetChase();
            return;
        }
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            pathIndex++;
            if(pathIndex >= points.Length)
            {
                pathIndex = 0;
            }

            SetPatrol();

            // Si queremos que se pare cuando llegue a un punto
            // SetIdle();
        }

        if (timeCounter >= moanTime)
        {
            zombieMoan.Play();
            zombieMoan.volume = Random.Range(0.8f, 0.9f);
            zombieMoan.volume = Random.Range(0.8f, 0.9f);
            moanTime = Random.Range(3.5f, 6.5f);
            timeCounter = 0;
        }
        else timeCounter += Time.deltaTime;
    }

    void ChaseUpdate()
    {
        agent.SetDestination(targetTransform.position);

        if(distanceFromTarget > chaseRange)
        {
            SetPatrol();
            return;
        }

        if(distanceFromTarget < attackRange)
        {
            SetAttack();
            return;
        }
    }

    void AttackUpdate()
    {
        agent.SetDestination(targetTransform.position);

        if(canAttack)
        {
            agent.Stop(); // 5.5 // agent.isStopped = true; // 5.6
            targetTransform.GetComponent<PlayerBehaviour>().SetDamage(hitDamage);
            idleTime = coolDownAttack;
            SetIdle();
            return;
        }

        if(distanceFromTarget > attackRange)
        {
            SetChase();
            return;
        }
    }

    void StunUpdate()
    {
        if (timeCounter >= stunTime)
        {
            idleTime = 0;
            SetIdle();
        }
        else timeCounter += Time.deltaTime;
    }

    void DeadUpdate()
    {

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
        agent.Resume();

        anim.SetBool("Walk", true);

        agent.speed = patrolSpeed;

        state = EnemyState.Patrol;

        agent.SetDestination(points[pathIndex].position);

        timeCounter = 0;
    }

    void SetChase()
    {
        // Feedback de información de persecución

        anim.SetBool("Walk", true);

        agent.speed = chaseSpeed;

        state = EnemyState.Chase;
    }

    void SetAttack()
    {
        // Feedback

        anim.SetTrigger("Attack");

        zombieAttack.Play();
        zombieAttack.pitch = Random.Range(0.98f, 1.03f);
        zombieAttack.volume = Random.Range(0.8f, 0.9f);

        state = EnemyState.Attack;
    }

    void SetStun()
    {
        agent.Stop();
        timeCounter = 0;
        anim.SetTrigger("ReceiveDMG");
        // Feedback, animación, sonido, etc.
        zombieReceiveDamage.Play();
        zombieReceiveDamage.pitch = Random.Range(0.98f, 1.03f);
        zombieReceiveDamage.volume = Random.Range(0.8f, 0.9f);
        state = EnemyState.Stun;
    }

    void SetDead()
    {
        anim.SetTrigger("Dead");
        agent.Stop();
        state = EnemyState.Dead;

        zombieDie.Play();
        zombieDie.pitch = Random.Range(0.98f, 1.03f);
        zombieDie.volume = Random.Range(0.8f, 0.9f);
        //Destroy(this.gameObject);
        // this.gameObject.SetActive(false);
    }

    #endregion

    #region Public functions

    public void SetDamage(int hit)
    {
        if (state == EnemyState.Dead) return;

        life -= hit;

        if (life <= 0)
        {
            SetDead();
            return;
        }
        SetStun();
    }

    #endregion

    float GetDistanceFromTarget()
    {
        return Vector3.Distance(targetTransform.position, transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
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
}
