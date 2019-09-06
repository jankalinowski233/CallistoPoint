using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [Header("Shooting")]
    [Space(5f)]
    public Transform m_shootPoint;
    public GameObject m_bulletPrefab;

    public void SpawnBullet()
    {
        GameObject go = Instantiate(m_bulletPrefab, m_shootPoint.position, m_shootPoint.rotation);
    }
}
