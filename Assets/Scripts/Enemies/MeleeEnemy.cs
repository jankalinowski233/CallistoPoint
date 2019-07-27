using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyController
{
    [Header("Melee combat")]
    [Space(7f)]
    public float m_fTimeBetweenMeleeAttacks;
    float m_fRemainingTimeBetweenMeleeAttacks;

    public override void Attack()
    {
        //play attack animation
        //play attack sound
        //play attack vfx

        base.Attack();
    }
}
