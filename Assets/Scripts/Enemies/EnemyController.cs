using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Character
{
    NavMeshAgent m_navAgent;

    [HideInInspector]
    public bool m_bHasTarget = false;
    bool m_bCanAttack;
    bool m_bIsWalking;

    GameObject m_gTarget;
    PlayerStats m_playerStats;

    public float m_fDamage;

    private void Awake()
    {
        m_navAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_gTarget = GameObject.FindGameObjectWithTag("Player");
        m_playerStats = PlayerStats.m_instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_bIsAlive == true)
        {
            if (m_bHasTarget == true)
            {
                WalkToTarget();

                if (HasReachedPath() == true)
                {
                    m_bCanAttack = true;
                }
                else m_bCanAttack = false;

                if (m_bCanAttack == true)
                    Attack();
            }
            else
            {
                //patrol
            }
        }
        
    }

    public virtual void Attack()
    {   
    }

    public void DealDamage()
    {
        m_playerStats.TakeDamage(m_fDamage);
    }

    void WalkToTarget()
    {
        m_navAgent.SetDestination(m_gTarget.transform.position);
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
