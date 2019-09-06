using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [Header("Shooting")]
    [Space(5f)]
    public Transform m_shootPoint;
    public GameObject m_bulletPrefab;
    public float m_fFiredBullets;

    public override void Attack()
    {
        GameObject go = Instantiate(m_bulletPrefab, m_shootPoint.position, m_shootPoint.rotation);
        m_fFiredBullets++;
        m_Anim.SetFloat("Fired bullets", m_fFiredBullets);
    }
}
