using UnityEngine;

public class AccessKeyHolder : Item
{
    public AccessKey m_accessKey;

    public override void Collect()
    {
        base.Collect();

        PlayerController player = PlayerController.m_instance;
        player.m_accesKeys.Add(m_accessKey);
    }
}
