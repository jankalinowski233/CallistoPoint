using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/OffensiveAbility")]
public class OffensiveAbility : Ability
{
    public GameObject m_gameObject;
    public float m_fForce;
    public float m_fDestroyDelay;

    OffensiveAbilityTrigger m_offensiveAbilityTrigger;

    public override void Initialize(GameObject obj)
    {
        m_offensiveAbilityTrigger = obj.GetComponent<OffensiveAbilityTrigger>();

        //initialize UI, sound etc. here

        m_offensiveAbilityTrigger.m_gameObject = m_gameObject;
        m_offensiveAbilityTrigger.m_fForce = m_fForce;
        m_offensiveAbilityTrigger.m_fDestroyDelay = m_fDestroyDelay;
        m_offensiveAbilityTrigger.m_Icon = m_Icon;
        m_offensiveAbilityTrigger.m_Sound = m_Sound;
    }

    public override void Cast()
    {
        m_offensiveAbilityTrigger.TriggerAbility();
    }
}
