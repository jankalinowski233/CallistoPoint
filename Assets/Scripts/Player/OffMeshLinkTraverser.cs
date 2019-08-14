using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class OffMeshLinkTraverser : MonoBehaviour
{
    Enemy m_enemy;
    GameObject m_previousTarget;

    private void Start()
    {
        if(gameObject.CompareTag("Enemy"))
            m_enemy = gameObject.GetComponent<Enemy>();

        StartCoroutine(CheckForOffMeshLink());
    }

    IEnumerator CheckForOffMeshLink()
    {
        NavMeshAgent navAgent = GetComponent<NavMeshAgent>();

        while (true)
        {
            if(navAgent.isOnOffMeshLink == true)
            {
                yield return StartCoroutine(TraverseOffMeshLink(navAgent));
                navAgent.CompleteOffMeshLink();

                if (m_enemy != null && m_enemy.GetTarget() == null)
                {
                    m_enemy.SetTarget(m_previousTarget);
                }
            }
            yield return null;
        }
    }

    IEnumerator TraverseOffMeshLink(NavMeshAgent agent)
    {
        OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 endLink = data.endPos + Vector3.up * agent.baseOffset;

        while (agent.transform.position != endLink)
        {
            if(m_enemy != null && m_enemy.GetTarget() != null)
            {
                m_previousTarget = m_enemy.GetTarget();
                m_enemy.SetTarget(null);
            }

            agent.transform.position = Vector3.MoveTowards(agent.transform.position, endLink, agent.speed * Time.deltaTime);
            transform.LookAt(endLink);

            yield return null;
        }
    }
}
