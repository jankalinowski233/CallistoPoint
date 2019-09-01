using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon stats")]
    [Space(7f)]
    [SerializeField] private float m_fShootingRange;
    [SerializeField] private float m_fWeaponDmg;
    public float m_fTimeBetweenShots;
    float m_fRemainingTimeBetweenShots;

    [Header("Reloading")]
    [Space(7f)]
    public int m_iMaxAmmo;
    public int m_iAmmoLeft;
    public int m_iMaxAmmoInMagazine;
    public int m_iAmmoLeftInMagazine;

    [Header("Weapon FX")]
    [Space(7f)]
    public ParticleSystem m_shotParticles;
    public ParticleSystem m_gunParticles;
    public ParticleSystem m_hitParticles;
    public Light m_gunLight;

    List<ParticleCollisionEvent> m_particleCollisionEvents;
    LineRenderer m_lineRenderer;
    AudioSource m_audio;

    private void Awake()
    {
        m_particleCollisionEvents = new List<ParticleCollisionEvent>();

        m_shotParticles.transform.position = m_gunParticles.transform.position;
 
        m_iAmmoLeftInMagazine = m_iMaxAmmoInMagazine;
        m_iAmmoLeft = m_iMaxAmmo;
        
        m_audio = GetComponent<AudioSource>();
        m_lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        m_lineRenderer.SetPosition(0, m_gunParticles.transform.position);
        m_lineRenderer.SetPosition(1, m_gunParticles.transform.position + transform.forward * m_fShootingRange);

    }

    public void Shoot()
    {
        if (m_fRemainingTimeBetweenShots <= 0)
        {
            if(m_iAmmoLeftInMagazine > 0)
            {
                //play weapon particle effects
                m_gunParticles.Stop();
                m_gunParticles.Play();

                //enable lighting
                m_gunLight.enabled = true;

                //enable line rendering

                //play sound
                m_audio.Play();
                m_shotParticles.Emit(1);

                //cast ray
                Ray ray = new Ray(transform.position, new Vector3(transform.forward.x, 0f, transform.forward.z));
                RaycastHit weaponHit;

                //check if it hit anything
                if (Physics.Raycast(ray, out weaponHit, m_fShootingRange, LayerMask.GetMask("Damageable")))
                {

                    if (weaponHit.collider.CompareTag("Enemy") || weaponHit.collider.CompareTag("Turret"))
                    {
                        //if it's an enemy, deal damage
                        Character character = weaponHit.collider.GetComponent<Character>();
                        character.TakeDamage(m_fWeaponDmg);
                    }

                }
                
                m_iAmmoLeftInMagazine--;

                UIManager.m_instance.SetAmmoText(m_iAmmoLeftInMagazine, m_iAmmoLeft);

                m_fRemainingTimeBetweenShots = m_fTimeBetweenShots;

                //disable vfx
                if (gameObject.activeInHierarchy == true)
                {
                    StartCoroutine(DisableVFX());
                }
            }
            else
                UIManager.m_instance.SetMessageText("No ammo left!");
        }
        else
        {
            m_fRemainingTimeBetweenShots -= Time.deltaTime;
        }
            

    }

    void DisableEffects()
    {
        m_gunLight.enabled = false;
    }

    public IEnumerator DisableVFX()
    {
        yield return new WaitForSeconds(0.05f);
        DisableEffects(); 
    }

    public void ResetTimeBetweenShots()
    {
        m_fRemainingTimeBetweenShots = 0f;
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

            UIManager.m_instance.SetAmmoText(m_iAmmoLeftInMagazine, m_iAmmoLeft);
        }
        else
            UIManager.m_instance.SetMessageText("You have no more ammo!");


    }

    public void AddAmmo(int amount)
    {
        //add ammo upon pick up
        if(m_iAmmoLeft < m_iMaxAmmo)
        {
            if(m_iAmmoLeft + amount <= m_iMaxAmmo)
            {
                m_iAmmoLeft += amount;
            }
            else if(m_iAmmoLeft + amount > m_iMaxAmmo)
            {
                int difference = m_iMaxAmmo - m_iAmmoLeft;
                m_iAmmoLeft += difference;
            }

            if(gameObject.activeInHierarchy == true)
                UIManager.m_instance.SetAmmoText(m_iAmmoLeftInMagazine, m_iAmmoLeft);
        }
    }

    private void OnEnable()
    {
        //make sure effects are disabled when switching weapon
        DisableEffects();
        UIManager.m_instance.SetAmmoText(m_iAmmoLeftInMagazine, m_iAmmoLeft);
    }
}
