using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float m_speed;
    public Vector3 m_direction;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(m_speed * m_direction * Time.deltaTime);
    }
}
