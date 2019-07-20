using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 v_offset;

    public Transform mt_Player;

    private void LateUpdate()
    {
        transform.position = mt_Player.position + v_offset;
    }
}
