using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    [HideInInspector] public bool m_bIsAlive = true;

    [Header("Health")]
    [Space(7f)]
    protected float m_fRemainingHealth;
    public float m_fMaxHealth;

    void Start()
    {
        m_fRemainingHealth = m_fMaxHealth;
    }

    public void TakeDamage(float dmg)
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

        if (m_bIsAlive == false)
        {
            //disable components
            //play sound
            //play vfx
            //return to main menu
        }
    }

}
