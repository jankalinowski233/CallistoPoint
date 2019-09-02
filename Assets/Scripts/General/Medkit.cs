using UnityEngine;

public class Medkit : Item
{
    public float m_fHealthAmount;

    public override void Collect()
    {
        PlayerStats.m_instance.Heal(m_fHealthAmount);

        base.Collect();
    }
}
