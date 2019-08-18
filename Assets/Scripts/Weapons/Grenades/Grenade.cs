using System.Collections;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GrenadeTemplate m_grenadeType;

    [HideInInspector] public float m_fDamage;
    [HideInInspector] public float m_fExplosionRadius;
    [HideInInspector] public float m_fEffectDuration;

    [HideInInspector] public GameObject m_particleSystem;
    [HideInInspector] public Sprite m_icon;
    [HideInInspector] public AudioClip m_explosionSound;

    Vector3 targetPos;

    public float m_flightHeight;
    public float m_flightDuration;

    private void OnEnable()
    {
        m_grenadeType.Initialize(this.gameObject);

        StartCoroutine(MoveToTarget(SetTargetPos(), m_flightHeight, m_flightDuration));
    }

    public void Create()
    {
        //grabbing references etc. goes here
    }

    public Vector3 SetTargetPos()
    {
        Ray ray = PlayerController.m_instance.m_mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;

        if(Physics.Raycast(ray, out rayHit))
        {
            targetPos = new Vector3(rayHit.point.x, 0f, rayHit.point.z);
            return targetPos;
        }

        return new Vector3(0, 0, 0);
    }

    IEnumerator MoveToTarget(Vector3 targetPos, float flightHeight, float flightDuration)
    {
        Vector3 startPos = transform.position;
        float normalizedTime = 0.0f;

        while(normalizedTime < 1.0f)
        {
            float yAxisOffset = flightHeight * (normalizedTime - normalizedTime * normalizedTime);
            transform.position = Vector3.Lerp(startPos, targetPos, normalizedTime) + yAxisOffset * Vector3.up;
            normalizedTime += Time.deltaTime / flightDuration;
            yield return null;
        }
        
    }

    public void Explode()
    {
        //play particle system, deal damage to enemies. apply effect to enemies etc.
        print("KA-BOOM");
        Destroy(gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Environment") || collision.collider.CompareTag("Enemy"))
        {
            Explode();
        }
    }
}
