using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset;
    Vector3 cameraPos;

    public Transform player;

    private void LateUpdate()
    {
        transform.position = player.position + offset;
    }
}
