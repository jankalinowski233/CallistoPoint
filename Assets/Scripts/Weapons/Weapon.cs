using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected float m_fTimeBetweenShots;
    protected float m_fRemainingTimeBetweenShots;

    Ray ray;
    RaycastHit rayHit;

    [Header("Weapon stats")]
    [Space(7f)]
    [SerializeField] private float m_fShootingRange;
    [SerializeField] private float m_fWeaponDmg;

    [Header("Reloading")]
    [Space(7f)]
    [SerializeField] private int m_iMaxAmmo;
    private int m_iAmmoLeft;

    private void Start()
    {
        m_iAmmoLeft = m_iMaxAmmo;
    }

    public virtual void Shoot()
    {
        if(m_iAmmoLeft > 0)
        {
            //basic shooting behaviour goes here
            if (m_fRemainingTimeBetweenShots <= 0)
            {
                //play weapon particle effects
                //enable some lighting
                //play sound

                //cast ray
                ray.origin = transform.position;
                ray.direction = transform.forward;

                //check if it hit anything
                if (Physics.Raycast(ray, out rayHit, m_fShootingRange, LayerMask.GetMask("Enemy")))
                {
                    //if it's an enemy, deal damage

                }
                else if (Physics.Raycast(ray, out rayHit, m_fShootingRange, LayerMask.GetMask("Environment")))
                {
                    //if it's environment, just spawn particle effect in the place it hit something
                    print("hitting environment");
                }

                m_iAmmoLeft--;
                //reset timer
                m_fRemainingTimeBetweenShots = m_fTimeBetweenShots;
            }
            else
            {
                //disable vfx

                //start counting timer
                m_fRemainingTimeBetweenShots -= Time.deltaTime;
            }
        }
        else
        {
            PlayerController.m_instance.Reload();
        }
        
    }

    public void ResetTimeBetweenShots()
    {
        m_fRemainingTimeBetweenShots = 0f;
    }

    public void RefillAmmo()
    {
        m_iAmmoLeft = m_iMaxAmmo;
    }
}
