using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : Character
{
    NavMeshAgent m_navAgent;
    [HideInInspector] public Animator m_Anim;

    bool m_bCanAttack;
    bool m_bIsWalking;

    GameObject m_gTarget;
    PlayerStats m_playerStats;

    public float m_fDamage;

    public ParticleSystem m_meleeHitEffect;

    Canvas m_enemyCanvas;
    public Image m_healthPoints;

    private void Awake()
    {
        m_navAgent = GetComponent<NavMeshAgent>();
        m_Anim = GetComponent<Animator>();

        m_enemyCanvas = GetComponentInChildren<Canvas>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_playerStats = PlayerStats.m_instance;

        m_healthPoints.fillAmount = m_fRemainingHealth / m_fMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bIsAlive == true && m_bIsStunned == false)
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
                    Chase();
                }

            }
            else
            {
                //patrol
            }
        }

        m_enemyCanvas.transform.LookAt(transform.position - PlayerController.m_instance.m_mainCamera.transform.rotation * Vector3.back, 
                                        PlayerController.m_instance.m_mainCamera.transform.rotation * Vector3.up);

        EffectTimer();
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

    public override void Stun(float stunDuration)
    {
        base.Stun(stunDuration);

        //play particle system, or set active some vortex, play animation etc.
    }

    public override void Scorch(float scorchDuration, float scorchDamage)
    {
        base.Scorch(scorchDuration, scorchDamage);

        //play particle system, or set active some scorch shader, play animation etc.
    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);

        if(m_gTarget == null && m_bIsStunned == false)
            SetTarget(m_playerStats.gameObject);

        m_healthPoints.fillAmount = m_fRemainingHealth / m_fMaxHealth;
    }

    public override void Kill()
    {
        base.Kill();
        LevelController.m_instance.RemoveFromList(this.gameObject);
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

    public GameObject GetTarget()
    {
        return m_gTarget;
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
