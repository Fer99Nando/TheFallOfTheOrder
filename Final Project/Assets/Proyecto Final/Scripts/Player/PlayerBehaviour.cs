using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private CharacterController controller;
    PlayerWeapon playerWeapon;
    private GameObject player;

    public Transform lookAt;

    public float forwardSpeed;              // Velocidad de avance
    private float diagonalForwardSpeed;     // Velocidad de avance en cada eje cuando avanza diagonalmente
    private float backSpeed;                // Velocidad de retroceso
    private float diagonalBackSpeed;        // Velocidad de retroceso en cada eje cuando retrocede diagonalmente
    public float gravity;                   // Gravedad

    private Vector3 moveDirection;          // Vector de la direccion

    private float inputV;                   // Tecla de avance recto
    private float inputH;                   // Tecla de avance lateral
    //private float jumpInput;
    private float attackTime;
    private float esquiveTime;
    private float esquiveSuma;
    private float cooldownTime;

    private bool dodgeTime;
    private bool dodgeTrue;
    private bool attackOne;
    public  bool canMove;
    public  bool canAttack;
    private bool godMode;

    public Animator anim;
    public AnimationClip attackAnim;

    PlayerHealth playerHealth;

    //public AudioSource footSteps;
    //public AudioSource

    // Use this for initialization
    void Start()
    {
        dodgeTime = false;
        canAttack = false;
        cooldownTime = 0;
        godMode = false;

        attackTime = attackAnim.length;
        attackTime *= 0.7f;

        anim = GetComponent<Animator>();
        this.controller = GetComponent<CharacterController>();
        playerHealth = GetComponent<PlayerHealth>();
        playerWeapon = GameObject.FindGameObjectWithTag("PlayerWeapon").GetComponent<PlayerWeapon>();
        player = GameObject.FindGameObjectWithTag("Player");

        canMove = true;

        this.diagonalForwardSpeed = (float)Mathf.Sqrt(this.forwardSpeed * this.forwardSpeed / 2);
        this.backSpeed = this.forwardSpeed / 2;
        this.diagonalBackSpeed = (float)Mathf.Sqrt(this.backSpeed * backSpeed / 2);

        this.moveDirection = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            if(godMode){
                this.controller.enabled = true;
                godMode = false;
            }else if(!godMode){
                this.controller.enabled = false;
                godMode = true;
            }
        }

        if (dodgeTrue)
        {
            esquiveSuma += Time.deltaTime/4;
            esquiveTime = esquiveSuma;

            player.transform.position += new Vector3(0, 0, esquiveTime);
        }

        if (godMode){
            GodMode();
        }

        if (Input.GetButtonDown("Attack_Melee"))
        {
            //if (cooldownTime == 0)
            //{
                canAttack = true;
                StartCoroutine(Attack());
            //}
        }

        if (canAttack)
        {
            cooldownTime += Time.deltaTime;
        }

        if (Input.GetButtonDown("Dodge"))
        {
            dodgeTrue = true;

            controller.center = new Vector3(0, -0.032f, 0.008f);
            controller.height = 0.013f;
            dodgeTime = true;
            anim.SetTrigger("Dodge 0");
        }

        if (dodgeTime)
        {
            forwardSpeed = 6f;
        }
        else
        {
            if (forwardSpeed < 8 && forwardSpeed > 0)
            {
                forwardSpeed+= Time.deltaTime * 4;
            }
            else if (forwardSpeed >= 8)
            {
                forwardSpeed = 8;
            }

        }

        if (canMove)
        {
            if (GetInput())
            {
                anim.SetBool("Walk", true);

                Rotate();
            }
            else anim.SetBool("Walk", false);

            Move();
        }

        if (cooldownTime >= 1)
        {
            canAttack = false;
            cooldownTime = 0;
        }
    }

    public void DodgeAcabado()
    {
        this.moveDirection = transform.TransformDirection(this.moveDirection);
        this.moveDirection.Set(0, 0, 0);
        dodgeTrue = false;
        esquiveSuma = 0;
        controller.height = 0.13f;
        controller.center = new Vector3(0, 0.007f, 0.008f);
        dodgeTime = false;
    }

    #region Coroutines
    IEnumerator Attack()
    {
        anim.SetTrigger("Attack");
        canMove = false;
        
        anim.SetBool("Walk", false);
        
        yield return new WaitForSeconds(attackTime);
        playerWeapon.BoxDisabled();
        canMove = true;
        //anim.SetBool("Walk", true);
    }
    #endregion 

    public void ColliderWeapon()
    {
        playerWeapon.BoxEnabled();
    }

    public void DisColliderWeapon()
    {
        playerWeapon.BoxDisabled();
    }
    

    // Escucha todas las teclas que controlan al jugador
    private bool GetInput()
    {        
        this.inputV = Input.GetAxis("Vertical");
        this.inputH = Input.GetAxis("Horizontal");

        if (this.inputV != 0 || this.inputH != 0)
        {
            return true;
        }
        return false;
    }

    // Mueve al personaje si se usan las teclas de control
    private void Move()
    {
        if (Grounded()) // Si el jugador esta en el suelo se puede mover y saltar
        {
            this.gravity = 1;

            if (this.inputV == 0 && this.inputH == 0) // QUIETO
            {
                this.moveDirection.Set(0, 0, 0);
            }

            else if (this.inputV > 0 && this.inputH == 0) // AVANZA
            {
                this.moveDirection.Set(0, 0, this.inputV * this.forwardSpeed);
            }

            else if (this.inputV < 0 && this.inputH == 0) // RETROCEDE
            {
                this.moveDirection.Set(0, 0, this.inputV * this.backSpeed);
            }

            else if (this.inputH > 0 && this.inputV == 0) // DERECHA
            {
                this.moveDirection.Set(this.inputH * this.forwardSpeed / 1.3f, 0, 0);
            }

            else if (this.inputH < 0 && this.inputV == 0) // IZQUIERDA
            {
                this.moveDirection.Set(this.inputH * this.backSpeed / 1.3f, 0, 0);
            }

            else if (this.inputV > 0 && this.inputH > 0) // AVANZA-DERECHA
            {
                this.moveDirection.Set(this.inputH * this.diagonalForwardSpeed, 0, this.inputV * this.diagonalForwardSpeed);
            }

            else if (this.inputV > 0 && this.inputH < 0) // AVANZA-IZQUIERDA
            {
                this.moveDirection.Set(this.inputH * this.diagonalForwardSpeed, 0, this.inputV * this.diagonalForwardSpeed);
            }

            else if (this.inputV < 0 && this.inputH > 0) // RETROCEDE-DERECHA
            {
                this.moveDirection.Set(this.inputH * this.diagonalBackSpeed, 0, this.inputV * this.diagonalBackSpeed / 1.3f);
            }

            else if (this.inputV < 0 && this.inputH < 0) // RETROCEDE-IZQUIERDA
            {
                this.moveDirection.Set(this.inputH * this.diagonalBackSpeed, 0, this.inputV * this.diagonalBackSpeed / 1.3f);
            }

            this.moveDirection = transform.TransformDirection(this.moveDirection); // Transformamos la direccion de loca a world space
        }

        else // EN EL ARIE
        {
            this.gravity = 9.81f;

            if ((this.controller.collisionFlags & CollisionFlags.Above) != 0) //Cuando choque la cabeza contra algo que rapidamente cambie a zero el salto y comience a caer
            {
                this.moveDirection.y = 0;
            }
        }
        this.moveDirection.y -= this.gravity * Time.deltaTime; // Le aplica gravedad constante a la direccion Y

        this.controller.Move(this.moveDirection * Time.deltaTime); // SE MUEVE
    }

    private void Rotate()
    {
        transform.rotation = Quaternion.Euler(0, this.lookAt.eulerAngles.y, 0);
    }

    private bool Grounded()
    {
        return Physics.Raycast(transform.position + this.controller.center, Vector3.down, this.controller.bounds.extents.y + 0.001f);
    }

    public void GodMode()
    {
        float movementH = Input.GetAxis("Horizontal");
        float movementV = Input.GetAxis("Vertical");
        if(Input.GetKey(KeyCode.Space)){
            transform.Translate(Vector3.up* 0.2f, Space.Self);
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(-Vector3.up * 0.2f, Space.Self);
        }
        transform.Translate(new Vector3(movementH* 0.2f, 0,movementV* 0.2f), Space.Self);
    }
}