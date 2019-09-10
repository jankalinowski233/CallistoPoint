using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : Turret
{
    LineRenderer m_line;
    public Transform m_laserStartPoint;

    public ParticleSystem m_laserBeamStart;

    public void Awake()
    {
        m_line = GetComponent<LineRenderer>();
    }

    public override void Idle()
    {
        base.Idle();

        if(m_line.enabled == true)
            m_line.enabled = false;

        if (m_laserBeamStart.isPlaying == true)
            m_laserBeamStart.Stop();
    }

    public override void AttackCurrentTarget()
    {
        if (m_line.enabled == false)
            m_line.enabled = true;

        if (m_laserBeamStart.isPlaying == false)
            m_laserBeamStart.Play();

        base.AttackCurrentTarget();

        m_line.SetPosition(0, m_laserStartPoint.position);
        m_line.SetPosition(1, new Vector3(m_hitEffect.transform.position.x, m_laserStartPoint.position.y, m_hitEffect.transform.position.z));
    }
}
