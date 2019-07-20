using UnityEngine;

public class PlayerStats : Character, IDamageable
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

    void Awake()
    {
        if(m_instance == null)
        {
            m_instance = this;
        }
    }

    void Start()
    {
        m_fRemainingHealth = m_fMaxHealth;
        print(m_fRemainingHealth);
    }

    public void Damage(float dmg)
    {
        //get dmg
        m_fRemainingHealth -= dmg;

        //do some stuff to make dmg more appealing

        if (m_fRemainingHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        //kill
        m_bIsAlive = false;

        if(m_bIsAlive == false)
        {
            //disable components
            //play sound
            //play vfx
            //return to main menu
        }
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
}
