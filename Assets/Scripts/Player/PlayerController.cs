using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public static PlayerController m_instance;

    [Header("Camera")]
    public Camera m_mainCamera;

    //other components
    NavMeshAgent m_navAgent;
    PlayerStats m_playerStats;
    Animator m_Anim;
    Weapon m_weapon;

    [Header("Weapons")]
    [Space(5f)]
    [SerializeField] private List<GameObject> ml_WeaponList = new List<GameObject>();
    private int m_iCurrentWeapon;
    [Range(0, 360)] public float m_fRifleAimOffset = 45f;

    [HideInInspector] public bool m_bIsWalking = false;
    [HideInInspector] public bool m_bIsAttacking = false;
    [HideInInspector] public bool m_bIsReloading = false;

    [Header("Melee combat")]
    [Space(5f)]
    public GameObject m_meleeWeapon;
    public Transform m_tMeleeAttackPoint;
    public float m_fMeleeRadius;
    public float m_fTimeBetweenMeleeAttacks;
    float m_fRemainingTimeBetweenMeleeAttacks;

    [Header("Movement WASD")]
    [Space(5f)]
    public float m_fMovementSpeed;
    Rigidbody m_rb;
    Vector3 m_vMovement;
    Vector3 m_vLookedAtPoint;

    [Header("Interaction")]
    [Space(5f)]
    [HideInInspector] public Interactable m_Interactable = null;

    [Header("Abilities")]
    [Space(5f)]
    AbilityTrigger[] m_Abilities;

    [Header("Grenades")]
    [Space(5f)]
    public GameObject m_grenade;
    public Transform m_grenadeSpawnPoint;
    [HideInInspector] public static Vector3 m_grenadeTargetPos;
    [HideInInspector] public int m_iGrenadesAmount;
    public float rotationSpeed = 5f;

    [Header("Inventory")]
    [Space(5f)]
    public List<AccessKey> m_accesKeys = new List<AccessKey>();

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        m_navAgent = GetComponent<NavMeshAgent>();
        m_Anim = GetComponent<Animator>();
        m_Abilities = GetComponents<AbilityTrigger>();

        if (m_instance == null) m_instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        ml_WeaponList[0].SetActive(true);
        m_iCurrentWeapon = ml_WeaponList.IndexOf(ml_WeaponList[0]);
        m_weapon = ml_WeaponList[m_iCurrentWeapon].GetComponent<Weapon>();

        if(m_grenade != null)
        {
            Grenade grenade = m_grenade.GetComponent<Grenade>();
            m_iGrenadesAmount = grenade.m_grenadeType.m_iAmount;
        }
        
        UIManager.m_instance.SetGrenadeText(m_iGrenadesAmount);

        m_playerStats = PlayerStats.m_instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_playerStats.m_bIsAlive == true || m_playerStats.m_bIsStunned == false)
        {
            ProcessMouseInput();
            ProcessKeyboardInput();
        }
    }

    void ProcessMouseInput()
    {
        //on LMB hold
        RotateAndShoot();

        //on mouse scroll
        ChooseWeapon();
    }

    void ProcessKeyboardInput()
    {
        WalkWASD();
        MeleeAttack();
        Reload();
        CastAbilities();
        ThrowGrenades();

        if (Input.GetKeyDown(KeyCode.E) && m_Interactable != null)
        {
            m_Interactable.Interact();
        }
    }

    void WalkWASD()
    {
        if(m_bIsReloading == false)
        {
            float xAxis = Input.GetAxisRaw("Horizontal");
            float yAxis = Input.GetAxisRaw("Vertical");

            m_vMovement.Set(xAxis, 0f, yAxis);

            m_vMovement = m_vMovement.normalized * m_fMovementSpeed * Time.deltaTime;
            BlendAnimations();

            m_rb.MovePosition(transform.position + m_vMovement);
        }
    }

    //blends animations by calculating dot products between player's rotation and player's movement direction
    void BlendAnimations()
    {
        float zAxisMagnitude = 0;
        float xAxisMagnitude = 0;

        if (m_vMovement.magnitude > 0)
        {
            m_bIsWalking = true;

            //normalize vector between mouse position and player's position
            Vector3 normalizedLookingAt = m_vLookedAtPoint - transform.position;
            normalizedLookingAt.Normalize();

            //calculate dot product between mouse position and player's movement direction on Z axis and clamp its value
            //to match blend tree variables
            zAxisMagnitude = Mathf.Clamp(Vector3.Dot(m_vMovement, normalizedLookingAt), -1, 1);

            //calculate dot product between mouse position and player's movement direction on X axis and clamp its value
            //to match blend tree variables
            Vector3 perpendicularLookingAt = new Vector3(normalizedLookingAt.z, 0, -normalizedLookingAt.x);
            xAxisMagnitude = Mathf.Clamp(Vector3.Dot(m_vMovement, perpendicularLookingAt), -1, 1);

            //set animator to run
            m_Anim.SetBool("RifleRun", m_bIsWalking);

        }
        else
        {
            //set animator back to idle
            m_bIsWalking = false;
            m_Anim.SetBool("RifleRun", m_bIsWalking);
        }

        //feed animator the parameters
        m_Anim.SetFloat("VelX", xAxisMagnitude);
        m_Anim.SetFloat("VelY", zAxisMagnitude);
    }

    void RotateAndShoot()
    {
        if (IsMouseOverUI() == false)
        {
            //rotate
            Ray ray = m_mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            Quaternion rotation;

            if (Physics.Raycast(ray, out rayHit))
            {
                Vector3 characterToMouseVector = (rayHit.point - transform.position);
                characterToMouseVector.y = 0f;

                m_vLookedAtPoint = rayHit.point;

                rotation = Quaternion.LookRotation(characterToMouseVector);
                rotation *= Quaternion.AngleAxis(m_fRifleAimOffset, Vector3.up);
                transform.rotation = rotation;

                if (Input.GetMouseButton(0))
                {
                    if (m_weapon.m_iAmmoLeftInMagazine > 0)
                    {
                        if (m_bIsAttacking == false)
                        {
                            m_bIsAttacking = true;
                        }

                        //shoot
                        if (m_weapon.m_iAmmoLeftInMagazine > 0)
                        {
                            if(m_bIsWalking == false)
                            {
                                if (m_iCurrentWeapon == 0)
                                {
                                    m_Anim.SetBool("ShootingSMG", true);
                                }
                                else m_Anim.SetBool("ShootingRifle", true);
                            }
                           
                            m_weapon.Shoot();
                            //TODO add sniper rifle
                        }
                        else
                        {
                            if(m_bIsWalking == false)
                            {
                                if (m_iCurrentWeapon == 0)
                                {
                                    m_Anim.SetBool("ShootingSMG", false);
                                }
                                else m_Anim.SetBool("ShootingRifle", false);

                                //TODO add sniper rifle
                            }

                            //TODO set UI no ammo or sth
                        }

                    }
                }

                //on LMB lift
                if (Input.GetMouseButtonUp(0))
                {
                    if (m_iCurrentWeapon == 0)
                    {
                        m_Anim.SetBool("ShootingSMG", false);
                    }
                    else m_Anim.SetBool("ShootingRifle", false);

                    m_weapon.ResetTimeBetweenShots();
                    m_bIsAttacking = false;
                }
            }
        }
    }

    bool IsMouseOverUI()
    {
        PointerEventData currentEventData = new PointerEventData(EventSystem.current);
        currentEventData.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(currentEventData, raycastResults);
        return raycastResults.Count > 0;
    }

    void Shoot()
    {      
        if(m_weapon != null)
        {
            m_weapon.Shoot();
        }
        else
        {
            Debug.LogError("Weapon is unassigned - can't shoot!");
        }
    }

    void ThrowGrenades()
    {
        if(Input.GetKeyDown(KeyCode.G) && m_bIsWalking == false && m_bIsAttacking == false && m_bIsReloading == false)
        {
            if (m_iGrenadesAmount > 0 && m_grenade != null)
            {
                Ray ray = m_mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit rayHit;

                if (Physics.Raycast(ray, out rayHit))
                {
                    m_grenadeTargetPos = rayHit.point;
                    m_grenadeTargetPos.y = 0f;
                    StartCoroutine(RotateTowards(rayHit.point, rotationSpeed));

                    //throw grenade
                    m_Anim.SetTrigger("ThrowGrenade");

                    m_iGrenadesAmount--;
                    m_bIsAttacking = true;

                    m_weapon.gameObject.SetActive(false);

                    UIManager.m_instance.SetGrenadeText(m_iGrenadesAmount);
                }       
            }
            else
            {
                UIManager.m_instance.SetMessageText("No grenades left!");
            }
                
        }
    }

    public void SpawnGrenade()
    {
        Instantiate(m_grenade, m_grenadeSpawnPoint.position, Random.rotation);
    }

    public IEnumerator RotateTowards(Vector3 target, float speed)
    {
        Quaternion desiredRotation = Quaternion.LookRotation(target - transform.position);
        float normalizedTime = 0.0f;

        while (normalizedTime < 1.0f)
        {
            //rotate towards target
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, normalizedTime);
            normalizedTime += Time.deltaTime * speed;
            yield return null;
        }

        yield break;
    }

    void ChooseWeapon()
    {
        if(m_bIsAttacking == false && m_bIsReloading == false)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (m_iCurrentWeapon < ml_WeaponList.Count - 1)
                {
                    if (m_iCurrentWeapon == 0)
                    {
                        m_Anim.SetTrigger("ChangeIdle");
                    }

                    m_iCurrentWeapon++;
                    ml_WeaponList[m_iCurrentWeapon].SetActive(true);
                    m_weapon = ml_WeaponList[m_iCurrentWeapon].GetComponent<Weapon>();
                    ml_WeaponList[m_iCurrentWeapon - 1].SetActive(false);
                }
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (m_iCurrentWeapon > 0)
                {
                    if (m_iCurrentWeapon == 1)
                    {
                        m_Anim.SetTrigger("ChangeIdle");
                    }

                    m_iCurrentWeapon--;
                    ml_WeaponList[m_iCurrentWeapon].SetActive(true);
                    m_weapon = ml_WeaponList[m_iCurrentWeapon].GetComponent<Weapon>();
                    ml_WeaponList[m_iCurrentWeapon + 1].SetActive(false);
                }
            }
        }   
    }

    public void CastAbilities()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_Abilities[0].m_Ability.Cast();
        }
    }

    void MeleeAttack()
    {
        if (m_fRemainingTimeBetweenMeleeAttacks <= 0)
        {
            if (Input.GetKey(KeyCode.F) && m_bIsWalking == false && m_bIsAttacking == false)
            {
                m_bIsAttacking = true;

                //play melee attack animation
                m_Anim.SetTrigger("MeleeAttack");

                //deactivate current gun
                m_weapon.gameObject.SetActive(false);

                //activate sword
                m_meleeWeapon.SetActive(true);

                //play melee attack sound
                //play melee attack vfx

                //reset timer
                m_fRemainingTimeBetweenMeleeAttacks = m_fTimeBetweenMeleeAttacks;
            }
        }
        else
        {
            //disable vfx
            StartCoroutine(DisableMeleeVFX());

            //start counting time
            m_fRemainingTimeBetweenMeleeAttacks -= Time.deltaTime;
        }
        
    }

    void DoMeleeDamage()
    {
        Collider[] enemies = Physics.OverlapSphere(m_tMeleeAttackPoint.position, m_fMeleeRadius, LayerMask.GetMask("Damageable"));
        foreach (Collider enemy in enemies)
        {
            //deal dmg to the enemy
            if(enemy.CompareTag("Enemy"))
            {
                Enemy damagedEnemy = enemy.GetComponent<Enemy>();
                damagedEnemy.TakeDamage(m_playerStats.m_fMeleeDamage);

                //add particle effect of taking damage
                damagedEnemy.m_meleeHitEffect.Play();
            }
        }
    }

    public void ReturnToIdle()
    {
        m_Anim.SetInteger("IdleValue", m_iCurrentWeapon);
        m_bIsAttacking = false;

        //reactivate gun
        m_weapon.gameObject.SetActive(true);
    }

    void FinishMeleeAttack()
    {
        ReturnToIdle();

        //deactivate sword
        m_meleeWeapon.SetActive(false);
    }

    IEnumerator DisableMeleeVFX()
    {
        yield return new WaitForSeconds(0.05f);
        //disable vfx here
    }

    public void MoveTo(Vector3 destination)
    {
        m_navAgent.SetDestination(destination);
    }

    public void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && m_bIsWalking == false && m_bIsAttacking == false)
        {
            if (m_weapon.m_iAmmoLeft == 0)
            {
                //display warning of no ammo left

                return;
            }
            else if (m_weapon.m_iAmmoLeftInMagazine == m_weapon.m_iMaxAmmoInMagazine)
            {
                //display warning of full magazine

                return;
            }

            if (m_bIsReloading == false)
            {
                m_bIsReloading = true;
                //play rifle animation of reloading
                m_Anim.SetTrigger("RifleReload");
            }
        } 
        
    }

    public void RefillAmmo()
    {
        m_weapon.RefillAmmo();
        m_bIsReloading = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Interactable"))
        {
            m_Interactable = other.gameObject.GetComponent<Interactable>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Interactable"))
        {
            m_Interactable = null;
        }
    }

    private void OnDrawGizmos()
    {
        //gizmos to visualize melee attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(m_tMeleeAttackPoint.position, m_fMeleeRadius);
    }
}
