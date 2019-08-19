using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon stats")]
    [Space(7f)]
    [SerializeField] private float m_fShootingRange;
    [SerializeField] private float m_fWeaponDmg;

    [Header("Reloading")]
    [Space(7f)]
    public int m_iMaxAmmo;
    public int m_iAmmoLeft;
    public int m_iMaxAmmoInMagazine;
    public int m_iAmmoLeftInMagazine;

    [Header("Weapon FX")]
    [Space(7f)]
    public ParticleSystem m_gunParticles;
    public GameObject m_environmentHitEffect;
    public GameObject m_damageEffect;
    public Light m_gunLight;
    LineRenderer m_lineRenderer;

    AudioSource m_audio;

    private void Awake()
    {
        m_iAmmoLeftInMagazine = m_iMaxAmmoInMagazine;
        m_iAmmoLeft = m_iMaxAmmo;

        m_lineRenderer = GetComponent<LineRenderer>();
        m_audio = GetComponent<AudioSource>();
    }

    public void Shoot()
    {
        if (m_iAmmoLeftInMagazine > 0)
        {
            //play weapon particle effects
            m_gunParticles.Stop();
            m_gunParticles.Play();

            //enable lighting
            m_gunLight.enabled = true;

            //enable line rendering
            m_lineRenderer.enabled = true;
            m_lineRenderer.SetPosition(0, transform.position);

            //play sound
            m_audio.Play();

            //cast ray
            Ray ray = new Ray(transform.position, new Vector3(transform.forward.x, 0f, transform.forward.z));
            RaycastHit weaponHit;
            
            //check if it hit anything
            if (Physics.Raycast(ray, out weaponHit, m_fShootingRange, LayerMask.GetMask("Damageable")))
            {
                m_lineRenderer.SetPosition(1, weaponHit.point);

                if (weaponHit.collider.CompareTag("Enemy") || weaponHit.collider.CompareTag("Turret"))
                {
                    //if it's an enemy, deal damage
                    Character character = weaponHit.collider.GetComponent<Character>();
                    character.TakeDamage(m_fWeaponDmg);

                    GameObject damageEffect = Instantiate(m_damageEffect, weaponHit.point, Quaternion.LookRotation(weaponHit.normal));
                    Destroy(damageEffect, 1f);
                }
                else if (weaponHit.collider.CompareTag("Environment"))
                {
                    //if it's environment, just spawn particle effect in the place it hit something
                    print("hitting environment");

                    GameObject hitEffect = Instantiate(m_environmentHitEffect, weaponHit.point, Quaternion.LookRotation(weaponHit.normal)); 
                    Destroy(hitEffect, 1f);

                }

                
            }
            else
            {
                m_lineRenderer.SetPosition(1, ray.origin + ray.direction * m_fShootingRange);
            }


            m_iAmmoLeftInMagazine--;

            //disable vfx
            if (gameObject.activeInHierarchy == true)
            {
                StartCoroutine(DisableVFX());
            }
        }
    }

    void DisableEffects()
    {
        m_gunLight.enabled = false;
        m_lineRenderer.enabled = false;
    }

    public IEnumerator DisableVFX()
    {
        yield return new WaitForSeconds(0.05f);
        DisableEffects(); 
    }

    public void RefillAmmo()
    {
        if (m_iAmmoLeft > 0)
        {
            if (m_iAmmoLeftInMagazine == 0)
            {
                if(m_iAmmoLeft >= m_iMaxAmmoInMagazine)
                {
                    m_iAmmoLeftInMagazine = m_iMaxAmmoInMagazine;
                    m_iAmmoLeft -= m_iMaxAmmoInMagazine;
                }
                else
                {
                    m_iAmmoLeftInMagazine += m_iAmmoLeft;
                    m_iAmmoLeft = 0;
                }
            }
            else
            {
                int difference = m_iMaxAmmoInMagazine - m_iAmmoLeftInMagazine;

                if(m_iAmmoLeft >= difference)
                {
                    m_iAmmoLeftInMagazine += difference;
                    m_iAmmoLeft -= difference;
                }
                else
                {
                    m_iAmmoLeftInMagazine += m_iAmmoLeft;
                    m_iAmmoLeft = 0;
                }
            }
        }

    }

    public void AddAmmo(int amount)
    {
        //add ammo upon pick up
    }

    private void OnEnable()
    {
        //make sure effects are disabled when switching weapon
        DisableEffects();
    }
}
