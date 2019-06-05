using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHealth : MonoBehaviour 
{
    private float maxVirus = 100;

    PlayerBehaviour playerBehaviour;

    /*[Header("Material")]
    public Material mat1;
    public Material mat2;*/

    public GameObject volverDerrota;
    public GameObject volverVictoria;

    [Header("Shader")]
    public GameObject editorShader;
    private MaterialPropertyBlock block;
    private SkinnedMeshRenderer renderer;

    [Header("Life")]
	public float startingHp;
	public float startingV;
	
	[Header("Life InGame")]
	public float currentHp;
	public float currentV;

	[Header("Times")] 
	private float toxicTime;
	private float timeCount;
    private float virusTime = 1;

    [Header("Bars")]
    public Image healthSlider;
    public Image virusSlider;

    // Activamos el gameobject GameOver
    public VideoPlayer loser;
    public GameObject gameOver;
    public GameObject desactivarHud;
    public GameObject personajes;
    public GameObject armaTrail;
    public Image bloodHood;

    // Sonido muerte

    [Header("Animation")]
	Animator anim;

	// Sonidos

	PlayerBehaviour playerControl;

	public bool isDead;
	bool damaged;
	bool intoxicate = false;

    public Color virusColor;
    public Color normalColor;

    Tweener tween;


    void Awake()
	{
        block = new MaterialPropertyBlock();
        renderer = editorShader.GetComponent<SkinnedMeshRenderer>();
        renderer.GetPropertyBlock(block);

        volverDerrota.SetActive(false);
        volverVictoria.SetActive(false);

        playerBehaviour = GetComponent<PlayerBehaviour>();
        anim = GetComponent<Animator>();
		playerControl = GetComponent<PlayerBehaviour>();

		currentHp = startingHp;
        healthSlider.fillAmount = currentHp/startingHp;

		currentV = startingV;
        virusSlider.fillAmount = 0;
        isDead = false;
        
        SetRim(0);
    }
	
	// Update is called once per frame
	void Update () 
	{        
        if(currentV >= maxVirus)
        {            
            DyingByVirus();
        }

        if(intoxicate)
		{
			timeCount += Time.deltaTime;
			toxicTime += Time.deltaTime;
			Virus();
		}
    }

	void UpdateLifeUI ()
	{
        currentHp = Mathf.Clamp(currentHp, 0, startingHp);
        //healthSlider.DOFillAmount( (healthSlider.fillAmount = currentHp / startingHp), 1);

        float newFillAmount = currentHp / startingHp;
        if (tween != null && tween.IsPlaying()) tween.Kill();
        tween = healthSlider.DOFillAmount(newFillAmount, 0.2f);
        //healthSlider.DOFillAmount(healthSlider.fillAmount, 0.5f);
        
        //healthSlider.fillAmount = currentHp / startingHp;
    }
    void UpdateVirusUI()
    {        
        currentV = Mathf.Clamp(currentV, startingV, maxVirus);

        if (currentV >= maxVirus) SetRim(0.8f);
        else if(currentV >= maxVirus/2 && currentV < maxVirus) SetRim(0.5f);
        else if(currentV >= maxVirus/3 && currentV < maxVirus/2) SetRim(0.3f);
        else SetRim(0);

        virusSlider.fillAmount = currentV / maxVirus;
    }

    public void TakeVirus (float vDamage)
	{
        currentV += vDamage;
        intoxicate = true;

        UpdateVirusUI();
	}
    public void Damage(int hit)
    {
        currentHp -= hit;
        bloodHood.DOFade(1, 0.3f).OnComplete(TerminadoFeedback);
        //bloodHood.SetActive(true);

        if (currentHp <= 0 && !isDead)
        {
            Death();
        }

        UpdateLifeUI();
    }

    public void TerminadoFeedback()
    {
        bloodHood.DOFade(0, 0.3f);
    }

    void Virus()
	{
		if (currentHp > 0)
		{			
			if (timeCount >= 3f)
			{
				Debug.Log("Me sube el Virus");
				currentV += 10;

                UpdateVirusUI();
				
				timeCount = 0;
                return;
			}
			if (toxicTime >= 8)
			{
				intoxicate = false;
				toxicTime = 0;
			}
		}
	}

    #region POTIONS
    public void PotionHelath ()
    {
        currentHp += 40;        
        UpdateLifeUI();
    }

    public void PotionAntidoto ()
    {
        currentV -= 30;
        UpdateVirusUI();
    }

    public void PotionAllInOne ()
    {
        currentHp += 50;
        currentV -= 75;

        UpdateLifeUI();
        UpdateVirusUI();
    }
    #endregion

    public void DyingByVirus()
    {
        if (currentHp > 0)
        {
            currentHp -= Time.deltaTime * virusTime;
            UpdateLifeUI();
        }
        else  Death();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Arrow" || other.tag == "WeaponBoss")
        {
            float valueHit;
            valueHit = Random.value;

            if (valueHit > 0.5)
            {
                armaTrail.SetActive(false);
                playerBehaviour.canMove = false;
                anim.SetTrigger("Hit");
            }
            else
            {
                armaTrail.SetActive(false);
                return;
            }
        }

        if (other.tag == "MuerteVacio")
        {
            DieAcabado();
        }
    }

    public void HitAcabado()
    {
        playerBehaviour.DodgeAcabado();

        playerBehaviour.canMove = true;

        playerBehaviour.comboOn = false;
        playerBehaviour.cooldown = false;
        playerBehaviour.attackOn = false;
        playerBehaviour.attackOne = false;
        playerBehaviour.comboTwoOn = false;
        playerBehaviour.dodgeTime = false;
        playerBehaviour.dodgeTrue = false;
        playerBehaviour.canAttack = false;

        anim.ResetTrigger("FirstCombo");
        anim.ResetTrigger("SecondCombo");
        anim.ResetTrigger("ChargeAttack");
        anim.ResetTrigger("Attack");
        anim.ResetTrigger("Hit");
    }

    public void Death()
    {
        Debug.Log("Death");
        armaTrail.SetActive(false);
        isDead = true;
        anim.SetBool("Died", true);
       playerControl.enabled = false;
    }

    public void DieAcabado()
    {
        desactivarHud.SetActive(false);
        loser.Play();
        gameOver.SetActive(true);
        //Destroy(personajes);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        this.gameObject.SetActive(false);
        volverDerrota.SetActive(true);
        volverVictoria.SetActive(true);
    }

    void SetRim(float value)
    {
        if (value >= 0.8f) block.SetColor("_OutlineColor", virusColor);
        else block.SetColor("_OutlineColor", normalColor);

        block.SetFloat("_RimIntensity", value);
        renderer.SetPropertyBlock(block);
    }


}
