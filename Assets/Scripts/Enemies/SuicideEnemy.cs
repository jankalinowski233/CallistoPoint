using UnityEngine;

public class SuicideEnemy : Enemy
{
    [Header("Explosion")]
    [Space(5f)]
    public float m_fExplosionDistance;
    public GameObject m_explosionRangeIndicator;

    public ParticleSystem m_explosionVFX;

    public AudioClip m_selfDestructionClip;

    bool m_bSoundPlayed = false;

    public override void Start()
    {
        base.Start();
    }

    public void LoadExplosion()
    {
        if (m_bSoundPlayed == false)
        {
            m_audioSource.clip = m_selfDestructionClip;
            m_audioSource.Play();
            m_bSoundPlayed = true;
        }

        //increase sound volume
        if(m_audioSource.isPlaying == false)
        {
            if (m_audioSource.clip == m_selfDestructionClip)
            {
                m_audioSource.clip = null;
            }
        }

        //play explosion loading vfx

        //show explosion range
        if (m_explosionRangeIndicator.activeInHierarchy == false)
        { 
            m_explosionRangeIndicator.SetActive(true);
        }
               
    }

    public void Explode()
    {
        if (Vector3.Distance(GetTarget().transform.position, transform.position) <= m_fExplosionDistance)
        {
            base.m_playerStats.TakeDamage(m_fDamage);
        }

        //disable meshes
        m_meshRenderer.enabled = false;
        m_Anim.enabled = false;
        m_explosionRangeIndicator.SetActive(false);
        m_enemyCanvas.gameObject.SetActive(false);

        //play vfx
        m_explosionVFX.Play();

        //play sound

        //destroy gameobject after 2 seconds
        Destroy(gameObject, 2f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, m_fExplosionDistance);
    }
}
