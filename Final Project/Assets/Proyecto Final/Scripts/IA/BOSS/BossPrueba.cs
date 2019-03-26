using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossPrueba : MonoBehaviour
{
    private enum EnemyState {Parado, Idle, Chase, Attack }
    [SerializeField] private EnemyState state;

    private enum BossPhaseTwo { Transformation, ChaseTwo, AttackTwo, Dead }
    [SerializeField] private BossPhaseTwo stateTwo;

    private enum BossPhase { PhaseOne, PhaseTwo, Dead }
    [SerializeField] private BossPhase phase;

    private NavMeshAgent agent;

    private Vector3 targetPosition;
    private GameObject player;
    public GameObject colliderJumpAttack;


    public int bonusEnemyStats;

    public bool coolDown;
    public bool coolDownJump;

    CharacterController controller;
    WeaponBoss playerWeapon;
    JumpAttack jumpAttack;
    BossHealth enemyDeath;
    PlayerHealth playerHealth;

    //public ParticleSystem bossTransformation;
    //public GameObject bossTransformation;


    [Header("FeedBack")]
    public Material mat1;

    [Header("Paths")]

    public Transform[] points;
    public int pathIndex = 0;      // Rango de Persecucion
    public float attackRange;       // Rango de Ataque
    [SerializeField] private float distanceFromTarget = Mathf.Infinity;     // Distancia del target que puede ser hasta infinito

    [Header("Speeds")]

    public float rotationSpeed;
    public float chaseSpeed;        // Velocidad de Persecucion
    public float patrolSpeed;       // Velocidad  mientras Patrulla

    [Header("Timers")]

    public float idleTime;      // IDLE
    public float timeCounter = 0;  // Contador de tiempo

    public float RandomAttack = 0;   // Enfriamineto despues de atacar

    [Header("Stats")]

    //private bool canAttack = false;     // El ataque del enemigo desactivado

    [Header("Animation")]

    public Animator anim;           // Para poder poner Animaciones

    private void Awake()
    {
        //colliderJumpAttack.SetActive(false);
        coolDownJump = false;
        coolDown = false;
        //bossTransformation.SetActive(false);
        bonusEnemyStats = 10;
        agent = GetComponent<NavMeshAgent>();
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();        // Llamamos a las animaciones

        //playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerWeapon = GameObject.FindGameObjectWithTag("WeaponBoss").GetComponent<WeaponBoss>();
        jumpAttack = colliderJumpAttack.GetComponent<JumpAttack>();
        enemyDeath = GetComponent<BossHealth>();
    }


    // Update is called once per frame
    void Update()
    {
        if (enemyDeath.isDead == false)
        {
            targetPosition = player.transform.position - transform.position;
            targetPosition.y = 0;
            Quaternion newRotation = Quaternion.LookRotation(targetPosition);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotationSpeed * 0.1f);
        }
        else
        {
            agent.isStopped = true;
        }

        switch (phase)
        {
            case BossPhase.PhaseOne:
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
            break;
            case BossPhase.PhaseTwo:
                transform.GetComponentInChildren<SkinnedMeshRenderer>().material = mat1;
                switch (stateTwo)
                {
                    case BossPhaseTwo.Transformation:
                        Debug.Log("O DIOOMIITO SE TRANSFORMA");
                        anim.SetTrigger("PhaseTwo 0");
                        //bossTransformation.Play();
                        //bossTransformation.SetActive(true);
                        break;
                    case BossPhaseTwo.ChaseTwo:
                        //bossTransformation.SetActive(false);
                        //bossTransformation.Stop();
                        ChaseUpdateTwo();
                        break;
                    case BossPhaseTwo.AttackTwo:
                        ActionUpdateTwo();
                        break;
                    default:
                        break;
                }
            break;

        }

        if(coolDown)
        {
            anim.ResetTrigger("Action");
            anim.SetBool("IddleTime", true);
                
            timeCounter += Time.deltaTime; // Esto es si quiero que tenga un time para quese  enfrie y poderle atacar

            if(timeCounter > 2)
            {
                RandomAttack = Random.value;
                timeCounter = 0;
                coolDown = false;
                anim.SetBool("IddleTime", false);
                agent.isStopped = false;
            }
        }

        if(coolDownJump)
        {
            anim.ResetTrigger("Action2");
            anim.SetBool("JumpAttackCooldown", true);
                
            timeCounter += Time.deltaTime; // Esto es si quiero que tenga un time para quese  enfrie y poderle atacar

            if(timeCounter > 3)
            {
                RandomAttack = Random.value;
                timeCounter = 0;
                coolDownJump = false;
                anim.SetBool("JumpAttackCooldown", false);
                agent.isStopped = false;
            }
        }

    }

    void LateUpdate()
    {
        distanceFromTarget = GetDistanceFromTarget();
    }

    #region AllUpdatesStates

    void IdleUpdate()
    {
            state = EnemyState.Chase;
            return;
    }

    void ChaseUpdate()
    {
        // animacion de giro hacia el personaje
        anim.SetBool("Chase", true);

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
        agent.SetDestination(player.transform.position);

        if (distanceFromTarget < attackRange)
        {
            if(coolDown == false && RandomAttack < 0.5f)
            {
                Debug.Log("ATTACK");
                agent.isStopped = true;
                anim.SetTrigger("Action");
            }

            if(coolDown == false && RandomAttack > 0.5f)
            {
                Debug.Log("ATTACK");
                agent.isStopped = true;
                anim.SetTrigger("Action2");
            }
            return;
        }
        else
        {
            Debug.Log("Salio Del Range");
            agent.isStopped = false;
            SetChase();
            return;
        }
    }

    #endregion

    #region Sets

    public void SetIdle()
    {
        state = EnemyState.Idle;
    }

    public void SetChase()
    {
        // Animacion de caminar

        if ( phase == BossPhase.PhaseOne)
        {
            agent.speed = chaseSpeed;   // La velocidad del enemigo pasa a ser igual que la de modo persecucion

            state = EnemyState.Chase;   // El estado pasa a ser persecucion
        }
        else
        {
            agent.speed = chaseSpeed;   // La velocidad del enemigo pasa a ser igual que la de modo persecucion

            stateTwo = BossPhaseTwo.ChaseTwo;   // El estado pasa a ser persecucion
        }

    }

    void SetAction()
    {
        if (phase == BossPhase.PhaseOne)
        {
            // Sonidos de Ataque si los tiene
            agent.stoppingDistance = 3.5f;
            state = EnemyState.Attack;
        }
        else
        {
            // Sonidos de Ataque si los tiene
            agent.stoppingDistance = 3.5f;
            stateTwo = BossPhaseTwo.AttackTwo;
        }
    }

    #endregion

    #region PhaseTwo
    public void AnimacionTerminada()
    {
        Debug.Log("Animacion terminada");

        attackRange = 4;
        controller.enabled = true;
        anim.SetBool("Chase", false);
        stateTwo = BossPhaseTwo.ChaseTwo;
    }
    public void ChangePhase()
    {
        bonusEnemyStats = 30;
        phase = BossPhase.PhaseTwo;
        stateTwo = BossPhaseTwo.Transformation;
    }
    
    void ChaseUpdateTwo()
    {
        // animacion de giro hacia el personaje
        if (enemyDeath.isDead == false)
        {
            anim.SetBool("Chase2", true);
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
        }

        if (distanceFromTarget < attackRange && enemyDeath.isDead == false)
        {
            anim.SetBool("Chase2", false);
            SetAction();
            return;
        }
    }

    void ActionUpdateTwo()
    {
        Debug.Log("TORTON Y AL SUELO");

        if (distanceFromTarget < attackRange && enemyDeath.isDead == false)
        {
            Debug.Log("ME CAGO EN TOOO");
            agent.isStopped = true;

            if (Random.value <= 0.5f && coolDown == false)
            {
                anim.SetTrigger("Action2");
            }

            else if(Random.value > 0.5f && coolDown == false)
            {
                agent.isStopped = true;
                anim.SetTrigger("Action1");
            }
        }
        else if (distanceFromTarget > attackRange && enemyDeath.isDead == false)
        {
            Debug.Log("Salio Del Range");
            agent.isStopped = false;
            SetChase();
            return;
        }
    }

    public void ComienzoAtaque()
    {
        playerWeapon.BoxEnabled();
    }

    public void FinalAtaque()
    {
        playerWeapon.BoxDisabled();
        coolDown = true;
    }

    public void ComienzoJumpAtaque()
    {
        jumpAttack.BoxEnabled();
        //colliderJumpAttack.SetActive(true);
    }

    public void FinalJumpAtaque()
    {
        jumpAttack.BoxDisabled();
        //colliderJumpAttack.SetActive(false);
        agent.isStopped = true;
        coolDownJump = true;
    }
    #endregion
    float GetDistanceFromTarget()       // Calcula la distancia con el player
    {
        return Vector3.Distance(player.transform.position, transform.position);
    }

    /*private void OnTriggerEnter(Collider col)
    {
        //col = colliderJumpAttack;
        if(col.CompareTag("Player"))
        {
            playerHealth.currentHp -= 50;
            colliderJumpAttack.SetActive(false);
        }
    }*/
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}