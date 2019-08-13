using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Raycast")]
    [Space(7f)]
    public Transform m_rayOrigin;

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
    public Light m_gunLight;
    LineRenderer m_lineRenderer;

    private void Awake()
    {
        m_iAmmoLeftInMagazine = m_iMaxAmmoInMagazine;
        m_iAmmoLeft = m_iMaxAmmo;

        m_lineRenderer = GetComponent<LineRenderer>();
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
            m_lineRenderer.SetPosition(0, m_rayOrigin.transform.position);

            //play sound

            //cast ray
            Ray ray = new Ray(m_rayOrigin.transform.position, m_rayOrigin.transform.forward);
            RaycastHit weaponHit;

            //check if it hit anything
            if (Physics.Raycast(ray, out weaponHit, m_fShootingRange, LayerMask.GetMask("Shootable")))
            {
                m_lineRenderer.SetPosition(1, weaponHit.point);

                if (weaponHit.collider.CompareTag("Enemy"))
                {
                    //if it's an enemy, deal damage
                    print("Dealing damage");
                }
                else if (weaponHit.collider.CompareTag("Environment"))
                {
                    //if it's environment, just spawn particle effect in the place it hit something
                    print("hitting environment");
                }
            }
            else
            {
                m_lineRenderer.SetPosition(1, ray.origin + ray.direction * m_fShootingRange);
            }

            m_iAmmoLeftInMagazine--;

            //disable vfx
            StartCoroutine(DisableVFX());
        }
    }

    IEnumerator DisableVFX()
    {
        yield return new WaitForSeconds(0.1f);
        m_gunLight.enabled = false;
        m_lineRenderer.enabled = false;
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
}
