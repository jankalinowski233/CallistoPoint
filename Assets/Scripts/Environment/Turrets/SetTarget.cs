using UnityEngine;

public class SetTarget : MonoBehaviour
{
    Turret m_turret;

    private void Start()
    {
        m_turret = GetComponentInParent<Turret>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Enemy e = other.GetComponent<Enemy>();

            m_turret.AddEnemy(e);
            e.m_EnemyDeathEvent += m_turret.RemoveEnemy;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Enemy e = other.GetComponent<Enemy>();

            if (m_turret.GetCurrentTarget() == e)
                m_turret.SetCurrentTarget(null);

            e.m_EnemyDeathEvent -= m_turret.RemoveEnemy;

            m_turret.RemoveEnemy(e);
        }
    }
}
