using UnityEngine;

public class GrenadeStash : Item
{
    public GameObject m_grenade;
    int m_iGrenadeAmount;

    public void Start()
    {
        m_iGrenadeAmount = m_grenade.GetComponent<Grenade>().m_grenadeType.m_iAmount;
    }

    public override void Collect()
    {
        //get player component
        PlayerController player = PlayerController.m_instance;

        //swap grenades
        player.m_grenade = m_grenade;

        //update player script and UI
        player.m_iGrenadesAmount = m_iGrenadeAmount;
        UIManager.m_instance.SetGrenadeText(player.m_iGrenadesAmount);

        base.Collect();
    }
}
