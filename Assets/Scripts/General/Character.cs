using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    [HideInInspector] public bool m_bIsAlive;
    [HideInInspector] public bool m_bIsStunned = false;
    [HideInInspector] public bool m_bIsScorching = false;

    [Header("Health")]
    [Space(7f)]
    protected float m_fRemainingHealth;
    public float m_fMaxHealth;
    [Range(0, 1)] public float m_fArmor = 1;

    float m_fRemainingEffectDurationTime;
    float m_fScorchingTime;
    float m_fScorchingDamage;

    void OnEnable()
    {
        m_bIsAlive = true;
        m_fRemainingHealth = m_fMaxHealth;
    }

    public virtual void TakeDamage(float dmg)
    {   
        //get dmg
        m_fRemainingHealth -= dmg * m_fArmor;

        //do some stuff to make taking dmg looking better

        if (m_fRemainingHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Stun(float stunDuration)
    {
        //stun
        m_bIsStunned = true;

        m_fRemainingEffectDurationTime = stunDuration;
    }

    public virtual void Scorch(float scorchDuration, float scorchDamage)
    {
        m_bIsScorching = true;

        m_fRemainingEffectDurationTime = scorchDuration;
        m_fScorchingDamage = scorchDamage;
    }

    public void EffectTimer()
    {
        if(m_bIsStunned == true || m_bIsScorching == true)
        {
            if (m_fRemainingEffectDurationTime <= 0)
            { 
                m_bIsStunned = false;
                m_bIsScorching = false;
            }
            else
            {
                m_fRemainingEffectDurationTime -= Time.deltaTime;

                if (m_bIsScorching == true)
                {
                    if (m_fScorchingTime <= 0f)
                    {
                        TakeDamage(m_fScorchingDamage);
                        m_fScorchingTime = 1.0f;
                    }
                    else
                    {
                        m_fScorchingTime -= Time.deltaTime;
                    }
                }
            }
        }
    }

    public virtual void Die()
    {
        //kill
        m_bIsAlive = false;

        print("dead");

        if (m_bIsAlive == false)
        {
            //disable components
            //play sound
            //play vfx
            //set ragdoll
        }
        else
        {
            Debug.LogError("Target still alive!");
            return;
        }
    }

}
