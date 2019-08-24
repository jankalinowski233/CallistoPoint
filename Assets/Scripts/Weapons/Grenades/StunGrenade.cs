using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunGrenade : Grenade
{
    public override void ApplyEffect()
    {
        base.ApplyEffect();

        //stun enemies

        Collider[] colliders = Physics.OverlapSphere(transform.position, m_fExplosionRadius);

        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Enemy"))
            {
                Enemy enemy = col.GetComponent<Enemy>();
                enemy.Stun(m_fEffectDuration);
            }
        }
    }
}
