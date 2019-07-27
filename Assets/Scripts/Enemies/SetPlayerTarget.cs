using UnityEngine;

public class SetPlayerTarget : MonoBehaviour
{
    EnemyController m_enemyController;

    private void Start()
    {
        m_enemyController = GetComponentInParent<EnemyController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            m_enemyController.m_bHasTarget = true;
        }
    }
}
