using UnityEngine;

public class PlayerStats : Character
{
    public static PlayerStats m_instance;

    [Header("Levelling")]
    [Space(7f)]
    public float m_fExperience;
    public float m_fNextLevelExperienceThreshold;
    public float m_fNextLevelExperienceThresholdChange;
    public float m_fOnLevelHealthChange;
    public uint m_uiSkillPoint;
    public uint m_uiLevel;

    [Header("Melee damage")]
    [Space(7f)]
    public float m_fMeleeDamage;

    void Awake()
    {
        if(m_instance == null)
        {
            m_instance = this;
        }

        UIManager.m_instance.m_healthPointSlider.minValue = 0;
        UIManager.m_instance.m_healthPointSlider.maxValue = m_fMaxHealth;

        
    }

    private void Start()
    {
        UIManager.m_instance.SetHealthValue(m_fRemainingHealth);
    }

    public void GetXP(float fExperiencePoints)
    {
        m_fExperience += fExperiencePoints;

        if(m_fExperience >= m_fNextLevelExperienceThreshold)
        {
            //gain level
            m_uiLevel++;
            m_fNextLevelExperienceThreshold += m_fNextLevelExperienceThresholdChange;
            m_uiSkillPoint++;

            //gain more health
            m_fMaxHealth += m_fOnLevelHealthChange;
            m_fRemainingHealth = m_fMaxHealth;

            //play vfx
            //play sound
        }
    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);

        UIManager.m_instance.SetHealthValue(m_fRemainingHealth);
    }

    public override void Kill()
    {
        base.Kill();

        //return to main menu
    }
}
