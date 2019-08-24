using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragGrenade : Grenade
{
    //frag grenade simply deals damage
    public override void ApplyEffect()
    {
        base.ApplyEffect();

        Collider[] colliders = Physics.OverlapSphere(transform.position, m_fExplosionRadius);

        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Enemy"))
            {
                Enemy enemy = col.GetComponent<Enemy>();
                enemy.TakeDamage(m_fDamage);
            }
        }
    }
}
