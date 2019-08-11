﻿using System.Collections;
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
    public int m_iMaxAmmo;
    public int m_iAmmoLeft;
    public int m_iMaxAmmoInMagazine;
    public int m_iAmmoLeftInMagazine;

    private void Awake()
    {
        m_iAmmoLeftInMagazine = m_iMaxAmmoInMagazine;
        m_iAmmoLeft = m_iMaxAmmo;

    }

    public void Shoot()
    {

        if (m_iAmmoLeftInMagazine > 0)
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
                    print("Dealing damage");
                }
                else if (Physics.Raycast(ray, out rayHit, m_fShootingRange, LayerMask.GetMask("Environment")))
                {
                    //if it's environment, just spawn particle effect in the place it hit something
                    print("hitting environment");
                }

                m_iAmmoLeftInMagazine--;

                //reset timer
                m_fRemainingTimeBetweenShots = m_fTimeBetweenShots;
            }
            else
            {
                //disable vfx

                //start counting time
                m_fRemainingTimeBetweenShots -= Time.deltaTime;
            }
        }
        
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
        }

    }

    public void PickAmmoUp()
    {
        //add ammo upon pick up
    }
}
