using UnityEngine;

[CreateAssetMenu]
public class GrenadeTemplate :  ScriptableObject
{
    public int m_iAmount;
    public float m_fDamage;
    public float m_fEffectDuration;
    public float m_fExplosionRadius;

    public GameObject m_particleSystem;
    public Sprite m_icon;
    public AudioClip m_explosionSound;

    public void Initialize(GameObject obj)
    {
        Grenade grenade = obj.GetComponent<Grenade>();
        grenade.Create();

        grenade.m_fDamage = m_fDamage;
        grenade.m_fExplosionRadius = m_fExplosionRadius;
        grenade.m_fEffectDuration = m_fEffectDuration;

        grenade.m_particleSystem = m_particleSystem;
        grenade.m_icon = m_icon;
        grenade.m_explosionSound = m_explosionSound;
    }
}
