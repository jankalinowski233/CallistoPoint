using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float m_fMoveSpeed;
    public float m_fDirectionChangeTime;
    public Vector3 m_MoveDirection;

    bool m_bMoveUp;

    void Start()
    {
        StartCoroutine(MoveUp());
    }

    private void Update()
    {
        if(m_bMoveUp == true)
        {
            transform.position += m_MoveDirection * m_fMoveSpeed * Time.deltaTime;
        }
        else
        {
            transform.position -= m_MoveDirection * m_fMoveSpeed * Time.deltaTime;
        }
    }

    IEnumerator MoveUp()
    {
        yield return new WaitForSeconds(m_fDirectionChangeTime);
        m_bMoveUp = true;

        yield return new WaitForSeconds(m_fDirectionChangeTime);
        m_bMoveUp = false;

        StartCoroutine(MoveUp());
    }
}

