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
    public GameObject ArmaTrail;

    public int bonusEnemyStats;

    public bool coolDown;
    public bool coolDownJump;

    public bool TimeToTransform;

    CharacterController controller;
    WeaponBoss playerWeapon;
    BossHealth enemyDeath;
    PlayerHealth playerHealth;
    JumpAttack jumpAttack;

    [Header("FeedBack")]
    //public Material mat1;
    public GameObject bossTransformation; // Transformation Particles
    public RFX4_EffectSettings bossPart; // Transformation Particles


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

    public float timeCounter = 0;  // Contador de tiempo

    public float RandomAttack = 0;   // Enfriamineto despues de atacar
    public float RandomChaseAttack = 0;   // Enfriamineto despues de atacar

    [Header("Stats")]

    //private bool canAttack = false;     // El ataque del enemigo desactivado

    [Header("Animation")]

    public Animator anim;           // Para poder poner Animaciones

    private void Awake()
    {
        ArmaTrail.SetActive(false);
        //colliderJumpAttack.SetActive(false);
        coolDownJump = false;
        coolDown = false;
        TimeToTransform = false;
        bossTransformation.SetActive(false);
        bossPart = bossTransformation.GetComponent<RFX4_EffectSettings>();
        bonusEnemyStats = 10;
        agent = GetComponent<NavMeshAgent>();
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();        // Llamamos a las animaciones

        //playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerWeapon = GameObject.FindGameObjectWithTag("WeaponBoss").GetComponent<WeaponBoss>();
        jumpAttack = colliderJumpAttack.GetComponent<JumpAttack>();
        enemyDeath = GetComponent<BossHealth>();
        bossPart.IsVisible = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (enemyDeath.isDead == false && coolDown == false && coolDownJump == false)
        {
            targetPosition = player.transform.position - transform.position;
            targetPosition.y = 0;
            Quaternion newRotation = Quaternion.LookRotation(targetPosition);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotationSpeed * 0.1f);
        }
        else if (enemyDeath.isDead)
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
                switch (stateTwo)
                {
                    case BossPhaseTwo.Transformation:
                        Debug.Log("O DIOOMIITO SE TRANSFORMA");
                        anim.SetTrigger("PhaseTwo 0");
                        bossTransformation.SetActive(true);
                        TimeToTransform = true;
                        agent.isStopped = true;
                        bossPart.IsVisible = true;

                        break;
                    case BossPhaseTwo.ChaseTwo:
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

        if(TimeToTransform)
        {
            controller.enabled = false;
            agent.isStopped = true;
        }

        if(coolDown)
        {
            anim.ResetTrigger("Action");
            anim.SetBool("IddleTime", true);
            anim.SetBool("Chase2", false);
                
            timeCounter += Time.deltaTime; // Esto es si quiero que tenga un time para quese  enfrie y poderle atacar

            if(timeCounter > 3 || distanceFromTarget >= 15)
            {
                //anim.ResetTrigger("Action");
                timeCounter = 0;
                coolDown = false;
                anim.SetBool("IddleTime", false);
                agent.isStopped = false;
                SetChase();
            }

            if (timeCounter < 0.5f)
            {
                RandomAttack = Random.value;
            }
        }

        if(coolDownJump)
        {
            anim.ResetTrigger("Action2");
            anim.SetBool("JumpAttackCooldown", true);
            anim.SetBool("Chase2", false);

            timeCounter += Time.deltaTime; // Esto es si quiero que tenga un time para quese enfrie y poderle atacar

            if(timeCounter > 5 || distanceFromTarget >= 15)
            {
                timeCounter = 0;
                coolDownJump = false;
                anim.SetBool("JumpAttackCooldown", false);
                agent.isStopped = false;
                SetChase();
            }

            if( timeCounter < 0.5f)
            {
                RandomAttack = Random.value;
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
        if (coolDown == false && coolDownJump == false)
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

            /*if (distanceFromTarget >= 10 && distanceFromTarget <= 15)
            {
                RandomChaseAttack = Random.value;
                if(RandomChaseAttack > 0.9)
                {
                    Debug.Log("ATTACK");
                    anim.SetTrigger("Action2");
                }
                else
                {
                }
            }*/
        }
    }

    void ActionUpdate()
    {
        if (distanceFromTarget < attackRange && enemyDeath.isDead == false)
        {
            if(coolDownJump == false && coolDown == false && RandomAttack < 0.4f)
            {
                ArmaTrail.SetActive(true);
                Debug.Log("ATTACK");
                anim.SetTrigger("Action");
                agent.isStopped = true;
            }

            if(coolDownJump == false && coolDown == false && RandomAttack > 0.4f && RandomAttack < 0.7f)
            {
                ArmaTrail.SetActive(true);
                Debug.Log("QUE SALTA");
                anim.SetTrigger("Action2");
            }

            if (coolDownJump == false && coolDown == false && RandomAttack > 0.7f)
            {
                ArmaTrail.SetActive(true);
                Debug.Log("OJO EL pinchazo");
                anim.SetTrigger("Action1");
            }
            return;
        }
        else if (distanceFromTarget > attackRange && enemyDeath.isDead == false)
        {
            ArmaTrail.SetActive(false);
            Debug.Log("Salio Del Range");
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
        
            if (phase == BossPhase.PhaseOne)
            {
                agent.speed = chaseSpeed;   // La velocidad del enemigo pasa a ser igual que la de modo persecucion

                state = EnemyState.Chase;   // El estado pasa a ser persecucion
            }
            else if (phase == BossPhase.PhaseTwo)
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
        else if (phase == BossPhase.PhaseTwo)
        {
            // Sonidos de Ataque si los tiene
            agent.stoppingDistance = 3.5f;
            stateTwo = BossPhaseTwo.AttackTwo;
        }
    }

    #endregion

    #region PhaseTwo

    public void InstantiatePart()
    {
        ArmaTrail.SetActive(true);
        jumpAttack.ParticlesLava();
    }

    public void FaseParticulas()
    {
        bossPart.IsVisible = false;
    }
    public void AnimacionTerminada()
    {
        Debug.Log("Animacion terminada");

        ArmaTrail.SetActive(false);
        bossTransformation.SetActive(false);
        anim.SetBool("Chase2", false);
        anim.ResetTrigger("Action2");
        anim.ResetTrigger("Action");
        anim.ResetTrigger("Action1");
        anim.ResetTrigger("PhaseTwo 0");
        //attackRange = 4;
        controller.enabled = true;
        agent.isStopped = false;
        TimeToTransform = false;
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
        if (coolDown == false && coolDownJump == false && enemyDeath.isDead == false && TimeToTransform == false)
        {
            // animacion de giro hacia el personaje

            if (distanceFromTarget > attackRange)
            {
                ArmaTrail.SetActive(false);
                anim.SetBool("Chase2", true);
                agent.SetDestination(player.transform.position);
            }

            if (distanceFromTarget < attackRange)
            {
                anim.SetBool("Chase2", false);
                SetAction();
                return;
            }
        }

            /*// animacion de giro hacia el personaje
            if (enemyDeath.isDead == false || coolDown == false || coolDownJump == false)
            {
                anim.SetBool("Chase2", true);

                if (distanceFromTarget > attackRange)
                {
                    agent.isStopped = false;
                    agent.SetDestination(player.transform.position);
                    return;
                }

                else if (distanceFromTarget < attackRange)
                {
                    anim.SetBool("Chase2", false);
                    SetAction();
                    return;
                }
            }*/
    }

    void ActionUpdateTwo()
    {
        if (distanceFromTarget < attackRange && enemyDeath.isDead == false && TimeToTransform == false)
        {
            if (coolDownJump == false && coolDown == false && RandomAttack < 0.3f)
            {
                Debug.Log("ATTACK");
                anim.SetTrigger("Action");
                agent.isStopped = true;
            }

            if (coolDownJump == false && coolDown == false && RandomAttack > 0.8f)
            {
                Debug.Log("ATTACK");
                anim.SetTrigger("Action2");
            }

            if (coolDownJump == false && coolDown == false && RandomAttack > 0.3f && RandomAttack < 0.5f)
            {
                ArmaTrail.SetActive(true);
                Debug.Log("OJO AREA");
                anim.SetTrigger("Action1");
            }

            if (coolDownJump == false && coolDown == false && RandomAttack > 0.5f && RandomAttack < 0.8f)
            {
                ArmaTrail.SetActive(true);
                Debug.Log("PINCHAZO");
                anim.SetTrigger("Action3");
            }
            return;
        }
        else if (distanceFromTarget > attackRange && enemyDeath.isDead == false)
        {
            Debug.Log("Salio Del Range");
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}