using UnityEngine;

public class AmmoCrate : Item
{
    public Weapon m_weapon;
    public int m_iAmount;

    public override void Collect()
    {
        if(m_weapon.m_iAmmoLeft < m_weapon.m_iMaxAmmo)
        {
            m_weapon.AddAmmo(m_iAmount);
            base.Collect();
        }
    }
}
