using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    [HideInInspector] public bool m_bIsAlive = true;

    [Header("Health")]
    [Space(7f)]
    protected float m_fRemainingHealth;
    public float m_fMaxHealth;

    void OnEnable()
    {
        m_fRemainingHealth = m_fMaxHealth;
    }

    public virtual void TakeDamage(float dmg)
    {   
        //get dmg
        m_fRemainingHealth -= dmg;

        //do some stuff to make taking dmg looking better

        if (m_fRemainingHealth <= 0)
        {
            Kill();
        }
    }

    public virtual void Kill()
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
