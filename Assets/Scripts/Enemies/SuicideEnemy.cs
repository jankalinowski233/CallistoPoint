using UnityEngine;

public class SuicideEnemy : Enemy
{
    [Header("Explosion")]
    [Space(5f)]
    public float m_fExplosionDistance;
    public GameObject m_explosionRangeIndicator;

    public ParticleSystem m_explosionVFX;

    Material m_flashMaterial;



    public void LoadExplosion()
    {
        //increase sound volume

        //play explosion loading vfx
        if(m_flashMaterial == null)
            m_flashMaterial = m_explosionRangeIndicator.GetComponent<MeshRenderer>().material;

        float currentFlashFrequency = m_flashMaterial.GetFloat("Vector1_956BD140");
        currentFlashFrequency += Time.deltaTime;
        m_flashMaterial.SetFloat("Vector1_956BD140", currentFlashFrequency);

        //show explosion range
        if (m_explosionRangeIndicator.activeInHierarchy == false)
                m_explosionRangeIndicator.SetActive(true);
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
