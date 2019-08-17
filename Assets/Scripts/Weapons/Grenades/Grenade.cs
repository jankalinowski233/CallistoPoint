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

    private void OnEnable()
    {
        m_grenadeType.Initialize(this.gameObject);
    }

    public void Create()
    {
        //grabbing references etc. goes here
    }

    IEnumerator MoveToTarget()
    {
        yield return null;
    }

    public void Explode()
    {
        //play particle system, deal damage to enemies. apply effect to enemies etc.
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Enemy"))
        {
            Explode();
        }
    }
}
