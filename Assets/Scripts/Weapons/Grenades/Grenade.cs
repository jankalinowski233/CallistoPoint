using System.Collections;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GrenadeTemplate m_grenadeType;

    [HideInInspector] public float m_fDamage;
    [HideInInspector] public float m_fExplosionRadius;
    [HideInInspector] public float m_fEffectDuration;

    [HideInInspector] public GameObject m_particleSystem;
    [HideInInspector] public Sprite m_icon;
    [HideInInspector] public AudioClip m_explosionSound;

    Vector3 targetPos;

    public float m_flightHeight;
    public float m_flightDuration;

    private void OnEnable()
    {
        PlayerController player = PlayerController.m_instance;
        m_grenadeType.Initialize(this.gameObject);

        StartCoroutine(MoveToTarget(PlayerController.m_grenadeTargetPos, m_flightHeight, m_flightDuration));

        Physics.IgnoreCollision(GetComponent<Collider>(), PlayerController.m_instance.GetComponent<Collider>());
    }

    public void Create()
    {
        //grabbing references etc. goes here
    }

    IEnumerator MoveToTarget(Vector3 targetPos, float flightHeight, float flightDuration)
    {
        Vector3 startPos = transform.position;
        float normalizedTime = 0.0f;

        while(normalizedTime < 1.0f)
        {
            float yAxisOffset = flightHeight * (normalizedTime - normalizedTime * normalizedTime);
            transform.position = Vector3.Lerp(startPos, targetPos, normalizedTime) + yAxisOffset * Vector3.up;
            normalizedTime += Time.deltaTime / flightDuration;
            yield return null;
        }
        
    }

    public void Explode()
    {
        //play particle system, deal damage to enemies. apply effect to enemies etc.
        GameObject go = Instantiate(m_particleSystem, transform.position, Quaternion.identity);

        AudioSource l_audioSource = go.AddComponent<AudioSource>();
        l_audioSource.PlayOneShot(m_explosionSound);

        Destroy(go, 2f);

        Collider[] colliders = Physics.OverlapSphere(transform.position, m_fExplosionRadius);

        foreach(Collider col in colliders)
        {
            if(col.CompareTag("Enemy"))
            {
                Enemy enemy = col.GetComponent<Enemy>();
                enemy.TakeDamage(m_fDamage);
            }
            
        }

        Destroy(gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Environment") || collision.collider.CompareTag("Enemy"))
        {
            Explode();
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_grenadeType.m_fExplosionRadius);
    }


}
