using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineTurret : Turret
{
    public ParticleSystem[] m_shotEffects;


    public override void AttackCurrentTarget()
    {
        base.AttackCurrentTarget();

        //TODO make particle systems

        //foreach(ParticleSystem ps in m_shotEffects)
        //{
        //    ps.Stop();
        //    ps.Play();
        //}
    }
}
