﻿using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    NavMeshAgent m_navAgent;
    [HideInInspector] public Animator m_Anim;

    bool m_bCanAttack;
    bool m_bIsWalking;

    GameObject m_gTarget;
    PlayerStats m_playerStats;

    public float m_fDamage;

    private void Awake()
    {
        m_navAgent = GetComponent<NavMeshAgent>();
        m_Anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_playerStats = PlayerStats.m_instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_bIsAlive == true)
        {
            if (m_gTarget != null)
            {
                WalkToTarget();

                if (HasReachedPath() == true)
                {
                    Attack();
                }
                else
                {

                }

            }
            else
            {
                //patrol
            }
        }
        
    }

    public virtual void Attack()
    {
        m_Anim.SetBool("Attack", true);
    }

    public virtual void Chase()
    {
        m_Anim.SetBool("Attack", false);
    }

    public void DealDamage()
    {
        if(m_gTarget == null)
        {
            Debug.LogError("Target is null!");
            return;
        }

        if (m_gTarget.CompareTag("Player"))
        {
            m_playerStats.TakeDamage(m_fDamage);
        }      
        else
        {
            //turret gets damage
        }

    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
        SetTarget(m_playerStats.gameObject);
    }

    public override void Kill()
    {
        base.Kill();
        Destroy(gameObject);
    }

    void WalkToTarget()
    {
        m_navAgent.SetDestination(m_gTarget.transform.position);
    }

    public void SetTarget(GameObject target)
    {
        m_gTarget = target;
        m_Anim.SetTrigger("TargetFound");
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
                    m_bCanAttack = true;
                    return true;
                }
            }
        }
        return false;
    }
}