﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    private CharacterController controller;
    PlayerWeapon playerWeapon;
    AreaDamage areaDamage;
    Inventory inventory;

    public Image cooldownFilled;

    public Transform lookAt;

    public GameObject armaTrail;
    public GameObject potion;

    public LayerMask groundMask;

    [Header("Speed & Direction")]

    public float forwardSpeed;              // Velocidad de avance
    private float diagonalForwardSpeed;     // Velocidad de avance en cada eje cuando avanza diagonalmente
    private float backSpeed;                // Velocidad de retroceso
    private float diagonalBackSpeed;        // Velocidad de retroceso en cada eje cuando retrocede diagonalmente
    public float gravity;                   // Gravedad

    private Vector3 moveDirection;          // Vector de la direccion

    [Header("Inputs")]

    private float inputV;                   // Tecla de avance recto
    private float inputH;                   // Tecla de avance lateral
    private float inputDodge;                   // Tecla de esquivar lateral

    [Header("Times")]

    private float attackTime;

    private float esquiveTime;

    private float cooldownTime;
    private float cooldownChargeTime;

    private float potionTime;

    [Header("Bools")]

    public bool dodgeTime;
    public bool dodgeTrue;

    public bool chargeAttack;

    public bool cooldown;

    public bool comboOn;
    public bool comboTwoOn;
    public bool attackOn;
    public bool attackOne;
    public bool canAttack;

    public bool canMove;
    private bool isWalking;
    
    private bool godMode;

    [Header("Animator")]

    public Animator anim;

    public AnimationClip potionAnim;

    [Header("Sonidos")]

    public AudioSource footSteps;
    public AudioSource axeSwing;
    public AudioClip footStepR;
    public AudioClip footStepL;
    public AudioClip hitSound;

    // Use this for initialization
    void Start()
    {
        cooldown = false;
        chargeAttack = false;
        comboTwoOn = false;
        comboOn = false;
        attackOn = false;

        dodgeTime = false;

        canAttack = false;
        cooldownTime = 0;
        godMode = false;

        potion.SetActive(false);
        armaTrail.SetActive(false);

        cooldownFilled.fillAmount = 0;

        inventory = GetComponent<Inventory>();
        anim = GetComponent<Animator>();
        this.controller = GetComponent<CharacterController>();
        playerWeapon = GameObject.FindGameObjectWithTag("PlayerWeapon").GetComponent<PlayerWeapon>();

        canMove = true;
        isWalking = false;

        this.diagonalForwardSpeed = (float)Mathf.Sqrt(this.forwardSpeed * this.forwardSpeed / 2);
        this.backSpeed = this.forwardSpeed / 2;
        this.diagonalBackSpeed = (float)Mathf.Sqrt(this.backSpeed * backSpeed / 2);

        this.moveDirection = Vector3.zero;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
       // if (paused) return;

        if (Input.GetKeyDown(KeyCode.F10))
        {
            if(godMode)
            {
                this.controller.enabled = true;
                godMode = false;
            }else if(!godMode){
                this.controller.enabled = false;
                godMode = true;
            }
        }

        if (dodgeTrue)
        {
            //Move();
            this.moveDirection.Set(this.inputH * this.forwardSpeed, 0, this.inputV * this.forwardSpeed);
            this.moveDirection = transform.TransformDirection(this.moveDirection);
        }

        if (godMode)
        {
            GodMode();
        }

        if (inventory.potionOn)
        {
            canMove = false;
            potionTime += Time.deltaTime;

            if (potionTime >= potionAnim.length)
            {
                inventory.potionAntidoto.SetActive(false);
                inventory.potionLife.SetActive(false);
                inventory.potionMix.SetActive(false);

                inventory.potionOn = false;
                potionTime = 0;
                canMove = true;
            }
        }

        if (Input.GetButtonDown("Charge_Attack") && !inventory.potionOn)
        {
            if (attackOn == false && cooldown == false)
            {
                armaTrail.SetActive(true);
                playerWeapon.attackStats += 15;
                cooldown = true;
                cooldownFilled.fillAmount = 1;

                attackOn = true;
                chargeAttack = true;

                anim.SetTrigger("ChargeAttack");
            }
        }

        if (Input.GetButtonDown("Attack_Melee") && !inventory.potionOn)
        {
            if (attackOn == false && cooldown == false)
            {
                Debug.Log("AtacoXD");
                armaTrail.SetActive(true);
                canAttack = true;
                attackOn = true;
                anim.SetTrigger("Attack");
            }

            if (comboOn)
            {
                armaTrail.SetActive(true);
                canAttack = true;
                anim.SetTrigger("FirstCombo");
                playerWeapon.bonusStats += 2;

            }

            if (comboTwoOn)
            {
                armaTrail.SetActive(true);
                canAttack = true;
                anim.SetTrigger("SecondCombo");
                playerWeapon.bonusStats += 3;
            }
        }

        if (canAttack)
        {
            canMove = false;
        
            anim.SetBool("Walk", false);
            cooldownTime += Time.deltaTime;
        }

        if (chargeAttack)
        {
            canMove = false;
        
            anim.SetBool("Walk", false);
        }

        if (Input.GetButtonDown("Dodge") && !inventory.potionOn)
        {
            if (dodgeTrue == false  && isWalking)
            {
                dodgeTrue = true;
                anim.SetTrigger("Dodge 0");
                controller.center = new Vector3(0, -0.032f, 0.008f);
                controller.height = 0.013f;
                //dodgeTime = true;
            } 
        }

        if (dodgeTime)
        {
            forwardSpeed = 4f;
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
                isWalking = true;
                anim.SetBool("Walk", true);

                anim.SetFloat("SpeedZ", inputV);
                anim.SetFloat("SpeedX", inputH);

                Rotate();
            }
            else
            {
                anim.SetBool("Walk", false);
                isWalking = false;
            }

            Move();
        }

        if (cooldown)
        {
            cooldownFilled.fillAmount -= Time.deltaTime/2.5f;

            if (cooldownTime >= 1)
            {
                cooldownFilled.fillAmount = 0;
                cooldown = false;
                cooldownTime = 0;
            }

            if (cooldownFilled.fillAmount == 0)
            {
                playerWeapon.attackStats -= 15;
                cooldownFilled.fillAmount = 0;
                cooldown = false;
            }
        }

        if (Time.timeScale != 0 && Input.GetMouseButtonDown(0)) Cursor.lockState = CursorLockMode.Locked;
        else if (Input.GetButtonDown("Cancel")) Cursor.lockState = CursorLockMode.None;
    }
    
    public void AudioAxeSwing()
    {
        axeSwing.pitch = Random.Range(0.95f, 1.05f);
        // axeSwing.volume = Random.Range(0.95f, 1.05f);
        axeSwing.Play();
    }

    public void WeaponDamage()
    {
        playerWeapon.Attack();
    }

    public void DodgeAcabado()
    {
        dodgeTrue = false;
        controller.height = 0.13f;
        controller.center = new Vector3(0, 0.007f, 0.008f);
        dodgeTime = false;
    }

    public void DrinkPotion()
    {
        anim.SetTrigger("Potion");
    }

    public void PotionAppear()
    {
        potion.SetActive(true);
    }

    public void PotionDisappear()
    {
        potion.SetActive(false);
    }

    #region Attack Combo
    public void AtaqueAcabado()
    {
        chargeAttack = false;
        canAttack = false;
        canMove = true;
        attackOn = false;
        anim.ResetTrigger("SecondCombo");
        anim.ResetTrigger("FirstCombo");
        anim.ResetTrigger("Attack");
        anim.ResetTrigger("ChargeAttack");
        armaTrail.SetActive(false);
    }

    public void SinCombo()
    {
        armaTrail.SetActive(false);
        attackOn = false;
        comboTwoOn = false;
        comboOn = false;
    }

    public void ComboComienzo()
    {
        comboOn = true;
    }

    public void ComboTwoComienzo()
    {
        comboTwoOn = true;
        comboOn = false;
    }
    #endregion
    

    // Escucha todas las teclas que controlan al jugador
    private bool GetInput()
    {        
        this.inputV = Input.GetAxis("Vertical");
        this.inputH = Input.GetAxis("Horizontal");
        //this.inputDodge = Input.GetAxis("Dodge");

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
                //transform.rotation = Quaternion.Euler(0, this.lookAt.eulerAngles.y, 0);
                this.moveDirection.Set(this.inputH * this.backSpeed / 1.2f, 0, 0);
            }

            else if (this.inputH < 0 && this.inputV == 0) // IZQUIERDA
            {
                this.moveDirection.Set(this.inputH * this.backSpeed / 1.2f, 0, 0);
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
                this.moveDirection.Set(this.inputH * this.diagonalBackSpeed, 0, this.inputV * this.diagonalBackSpeed / 1.2f);
            }

            else if (this.inputV < 0 && this.inputH < 0) // RETROCEDE-IZQUIERDA
            {
                this.moveDirection.Set(this.inputH * this.diagonalBackSpeed, 0, this.inputV * this.diagonalBackSpeed / 1.2f);
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
        return Physics.Raycast(transform.position + this.controller.center, Vector3.down, this.controller.bounds.extents.y + 0.001f, groundMask, QueryTriggerInteraction.Ignore);
    }

    public void ZapatazoR()
    {
        footSteps.pitch = Random.Range(0.95f, 1.05f);
        footSteps.volume = Random.Range(0.95f, 1.05f);

        footSteps.clip = footStepR;
        footSteps.Play();
    }

    public void ZapatazoL()
    {
        footSteps.pitch = Random.Range(0.95f, 1.05f);
        footSteps.volume = Random.Range(0.45f, 0.55f);

        footSteps.clip = footStepL;
        footSteps.Play();
    }

    public void HitPlayer()
    {
        footSteps.clip = hitSound;
        footSteps.Play();
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