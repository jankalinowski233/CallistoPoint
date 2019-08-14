using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class OffMeshLinkTraverser : MonoBehaviour
{
    NavMeshAgent m_navAgent;

    private void Start()
    {
        m_navAgent = GetComponent<NavMeshAgent>();

        StartCoroutine(CheckForOffMeshLink());
    }

    IEnumerator CheckForOffMeshLink()
    {
        while(true)
        {
            if(m_navAgent.isOnOffMeshLink == true)
            {
                yield return StartCoroutine(TraverseOffMeshLink());
                m_navAgent.CompleteOffMeshLink();
            }
            yield return null;
        }
    }

    IEnumerator TraverseOffMeshLink()
    {
        OffMeshLinkData data = m_navAgent.currentOffMeshLinkData;
        Vector3 endLink = data.endPos + Vector3.up * m_navAgent.baseOffset;

        while(m_navAgent.transform.position != endLink)
        {
            m_navAgent.transform.position = Vector3.MoveTowards(m_navAgent.transform.position, endLink, m_navAgent.speed * Time.deltaTime);
            transform.LookAt(endLink);
            yield return null;
        }
    }
}
