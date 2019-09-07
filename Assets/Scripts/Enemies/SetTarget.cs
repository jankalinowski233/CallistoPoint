using UnityEngine;

//useless now, may be useful later
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
            m_turret.AddEnemy(other.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Enemy e = other.GetComponent<Enemy>();

            if (m_turret.GetCurrentTarget() == e)
                m_turret.SetCurrentTarget(null);

            m_turret.RemoveEnemy(e);
        }
    }
}
