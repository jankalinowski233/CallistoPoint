using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : Character
{
    NavMeshAgent m_navAgent;
    [HideInInspector] public Animator m_Anim;

    GameObject m_gTarget;
    protected PlayerStats m_playerStats;
    protected SkinnedMeshRenderer m_meshRenderer;

    public float m_fDamage;

    public ParticleSystem m_meleeHitEffect;

    protected Canvas m_enemyCanvas;
    public Image m_healthPoints;

    private void Awake()
    {
        m_navAgent = GetComponent<NavMeshAgent>();
        m_Anim = GetComponent<Animator>();

        m_enemyCanvas = GetComponentInChildren<Canvas>();
        m_meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    // Start is called before the first frame update
    public void Start()
    {
        m_playerStats = PlayerStats.m_instance;
        m_gTarget = m_playerStats.gameObject;

        m_healthPoints.fillAmount = m_fRemainingHealth / m_fMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bIsAlive == true)
        {
            if(m_gTarget != null)
            {
                m_Anim.SetFloat("Distance", Vector3.Distance(transform.position, m_gTarget.transform.position));
            }
        }

        if(m_meshRenderer.isVisible == true)
        {
            m_enemyCanvas.transform.LookAt(transform.position - PlayerController.m_instance.m_mainCamera.transform.rotation * Vector3.back,
                                PlayerController.m_instance.m_mainCamera.transform.rotation * Vector3.up);
        }

        EffectTimer();
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

    public virtual void Attack()
    {
        //basic attack behaviour goes here
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

        if(m_bIsStunned == false)
            SetTarget(m_playerStats.gameObject);

        m_healthPoints.fillAmount = m_fRemainingHealth / m_fMaxHealth;
    }

    public override void Kill()
    {
        base.Kill();
        LevelController.m_instance.RemoveFromList(this.gameObject);
        Destroy(gameObject);
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

    //useless for now, but might be useful - left just in case
    public bool HasReachedPath()
    {
        //check if agent reached destination
        if (m_navAgent.pathPending == false)
        {
            if (m_navAgent.remainingDistance <= m_navAgent.stoppingDistance)
            {
                if (m_navAgent.hasPath == false || m_navAgent.velocity.sqrMagnitude == 0)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
