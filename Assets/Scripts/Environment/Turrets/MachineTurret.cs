using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineTurret : Turret
{
    public ParticleSystem[] m_shotEffects;

    public override void AttackCurrentTarget()
    {
        base.AttackCurrentTarget();

        if (m_fRemainingTimeBetweenDamage <= 0)
        {
            foreach (ParticleSystem ps in m_shotEffects)
            {
                ps.Stop();
                ps.Play();
            }
        }

    }
}
