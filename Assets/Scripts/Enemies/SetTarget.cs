using UnityEngine;

public class SetTarget : MonoBehaviour
{
    Enemy m_Enemy;

    private void Start()
    {
        m_Enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") || other.CompareTag("Turret"))
        {
            m_Enemy.SetTarget(other.gameObject);
        }
    }
}
