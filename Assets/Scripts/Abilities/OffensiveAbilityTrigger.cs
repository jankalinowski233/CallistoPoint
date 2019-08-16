using UnityEngine;

public class OffensiveAbilityTrigger : AbilityTrigger
{
    [HideInInspector] public GameObject m_gameObject;
    [HideInInspector] public float m_fForce;
    [HideInInspector] public float m_fDestroyDelay;

    public override void TriggerAbility()
    {
        if(m_bReady == true)
        {
            base.TriggerAbility();

            if (m_Ability.m_CastRay == true)
            {
                Ray ray = PlayerController.m_instance.m_mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit rayHit;

                if (Physics.Raycast(ray, out rayHit))
                {
                    GameObject go = Instantiate(m_gameObject, rayHit.point, Quaternion.identity);
                    Destroy(go, m_fDestroyDelay);
                }
            }
            else
            {
                GameObject go = Instantiate(m_gameObject, PlayerController.m_instance.transform.position, Quaternion.identity);
                Destroy(go, m_fDestroyDelay);
            }
        }
        
    }
}
