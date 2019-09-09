using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : Turret
{
    LineRenderer m_line;
    public Transform m_laserStartPoint;

    public ParticleSystem m_laserBeamStart;
    public ParticleSystem m_laserHit;

    public void Awake()
    {
        m_line = GetComponent<LineRenderer>();
    }

    public override void AttackCurrentTarget()
    {
        m_fTurretDamage *= Time.deltaTime;

        base.AttackCurrentTarget();

        m_line.SetPosition(0, m_laserStartPoint.position);
        m_line.SetPosition(1, m_currentTarget.transform.position);
    }
}
