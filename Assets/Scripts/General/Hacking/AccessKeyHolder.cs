using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessKeyHolder : Item
{
    public AccessKey accessKey;
    public override void Collect()
    {
        base.Collect();

        PlayerController player = PlayerController.m_instance;
        player.m_accesKeys.Add(accessKey);
    }
}
