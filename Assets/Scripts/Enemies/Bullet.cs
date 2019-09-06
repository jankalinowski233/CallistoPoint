using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float m_fSpeed;
    [SerializeField] float m_fDamage;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * m_fSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats.m_instance.TakeDamage(m_fDamage);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Environment"))
        {
            Destroy(gameObject);
        }
    }
}
