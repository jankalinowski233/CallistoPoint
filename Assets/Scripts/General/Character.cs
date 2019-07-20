using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    [HideInInspector] public bool m_bIsAlive = true;

    [Header("Health")]
    [Space(7f)]
    protected float m_fRemainingHealth;
    public float m_fMaxHealth;

    [Header("Combat")]
    [Space(7f)]
    public float m_fMeleeRadius;
    public float m_fMeleeDmg;
    public float m_fRangeDmg;

    public Transform m_tMeleeAttackPoint;
}
