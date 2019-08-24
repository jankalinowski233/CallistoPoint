using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorchGrenade : Grenade
{
    public override void ApplyEffect()
    {
        base.ApplyEffect();

        //scorch enemies
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_fExplosionRadius);

        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Enemy"))
            {
                Enemy enemy = col.GetComponent<Enemy>();
                enemy.Scorch(m_fEffectDuration, m_fDamage);
            }
        }
    }
}
