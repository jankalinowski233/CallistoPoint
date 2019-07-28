﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController m_instance;

    public Camera m_mainCamera;

    NavMeshAgent m_navAgent;
    PlayerStats m_playerStats;
    Animator m_Anim;
    Weapon m_weapon;

    [SerializeField] private List<GameObject> ml_WeaponList = new List<GameObject>();

    private int m_iCurrentWeapon;

    [HideInInspector] public bool m_bIsWalking = false;
    [HideInInspector] public bool m_bIsAttacking = false;

    [Header("Melee combat")]
    [Space(7f)]
    public Transform m_tMeleeAttackPoint;
    public float m_fMeleeRadius;
    public float m_fTimeBetweenMeleeAttacks;
    float m_fRemainingTimeBetweenMeleeAttacks;

    private void Awake()
    {
        m_navAgent = GetComponent<NavMeshAgent>();
        m_Anim = GetComponent<Animator>();

        if (m_instance == null) m_instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        ml_WeaponList[0].SetActive(true);
        m_iCurrentWeapon = ml_WeaponList.IndexOf(ml_WeaponList[0]);
        m_weapon = ml_WeaponList[m_iCurrentWeapon].GetComponent<Weapon>();

        m_playerStats = PlayerStats.m_instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_playerStats.m_bIsAlive == true)
        {
            ProcessMouseInput();
            ProcessKeyboardInput();

            if(m_bIsWalking == true)
            {
                HasReachedPath();
            }
        }
    }

    void ProcessMouseInput()
    {
        //on LMB hold
        if (Input.GetMouseButton(0) && m_bIsWalking == false)
        {
            if(m_bIsAttacking == false)
            {
                m_bIsAttacking = true;
            }

            Rotate();
            Shoot();

            if (m_iCurrentWeapon == 0)
            {
                m_Anim.SetBool("ShootingPistol", true);
            }
            else if (m_iCurrentWeapon == 1)
            {
                m_Anim.SetBool("ShootingSMG", true);
            }
            else m_Anim.SetBool("ShootingRifle", true);
        }

        //on single RMB click
        if (Input.GetMouseButtonDown(1) && m_bIsAttacking == false)
        {
            if (m_iCurrentWeapon == 0)
            {
                m_Anim.SetBool("PistolRun", true);
            }
            else m_Anim.SetBool("RifleRun", true);

            Walk();
        }

        //on LMB lift
        if(Input.GetMouseButtonUp(0))
        {
            m_bIsAttacking = false;
            m_weapon.ResetTimeBetweenShots();

            if (m_iCurrentWeapon == 0)
            {
                m_Anim.SetBool("ShootingPistol", false);
            }
            else if (m_iCurrentWeapon == 1)
            {
                m_Anim.SetBool("ShootingSMG", false);
            }
            else m_Anim.SetBool("ShootingRifle", false);
        }

        ChooseWeapon();
    }

    void ProcessKeyboardInput()
    {
        if(Input.GetKey(KeyCode.F) && m_bIsWalking == false)
        {
            if (m_bIsAttacking == false)
            {
                m_bIsAttacking = true;
            }
            MeleeAttack();
        }

        if(Input.GetKeyDown(KeyCode.R) && m_bIsWalking == false && m_bIsAttacking == false)
        {
            Reload();
        }
    }

    void Walk()
    {
        m_bIsWalking = true;   
        Ray ray = m_mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;

        if (Physics.Raycast(ray, out rayHit))
        {
            MoveTo(rayHit.point);
        }
    }

    void Rotate()
    {
        Ray ray = m_mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;

        if(Physics.Raycast(ray, out rayHit))
        {
            Vector3 clickPoint = rayHit.point;
            Vector3 characterToMouseVector = rayHit.point - transform.position;

            characterToMouseVector.y = 0f;

            Quaternion rotation = Quaternion.LookRotation(characterToMouseVector);
            transform.rotation = rotation;
        }
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

    void ChooseWeapon()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if(m_iCurrentWeapon < ml_WeaponList.Count - 1)
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
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
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

    void MeleeAttack()
    {
        //play melee attack animation
        //play melee attack sound
        //play melee attack vfx

        Collider[] enemies = Physics.OverlapSphere(m_tMeleeAttackPoint.position, m_fMeleeRadius, LayerMask.GetMask("Enemy"));
        foreach(Collider enemy in enemies)
        {
            //deal dmg to the enemy
        }
        m_bIsAttacking = false;
    }

    bool HasReachedPath()
    {
        //check if agent reached destination
        if (m_navAgent.pathPending == false)
        {
            if (m_navAgent.remainingDistance <= m_navAgent.stoppingDistance)
            {
                if (m_navAgent.hasPath == false || m_navAgent.velocity.sqrMagnitude == 0)
                {
                    m_bIsWalking = false;

                    if (m_iCurrentWeapon == 0)
                    {
                        m_Anim.SetBool("PistolRun", false);
                    }
                    else m_Anim.SetBool("RifleRun", false);

                    return true;
                }
            }
        }
        return false;
    }

    public void MoveTo(Vector3 destination)
    {
        m_navAgent.SetDestination(destination);
    }

    public void Reload()
    {
        if(m_weapon.m_iMaxAmmo == 0 || m_weapon.m_iAmmoLeft == m_weapon.m_iMaxAmmoInMagazine)
        {
            return;
        }
        //play animation of reloading
        if(m_iCurrentWeapon == 0)
        {
            //play pistol animation of reloading
        }
        else
        {
            //play rifle animation of reloading
            m_Anim.SetTrigger("RifleReload");
        }
        
        //make an animation event at some point to reload ammo
    }

    public void RefillAmmo()
    {
        m_weapon.RefillAmmo();
    }
}
