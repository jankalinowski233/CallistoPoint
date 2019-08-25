using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class DoorController : Interactable
{
    bool m_bIsOpened;

    Animator m_Anim;
    BoxCollider m_DetectArea;
    BoxCollider m_DoorCollider;

    [Header("Ceiling controller")]
    [Space(7f)]
    public GameObject m_Ceiling;
    [Range(0, 1)]
    public float m_fCeilingFadeSpeed = 1f;

    private void Awake()
    {
        m_bIsOpened = false;

        m_Anim = GetComponentInChildren<Animator>();
        m_DetectArea = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        //get box collider of a door
        BoxCollider[] boxColliders = GetComponentsInChildren<BoxCollider>();

        foreach(BoxCollider coll in boxColliders)
        {
            if(coll.transform.parent != null)
            {
                m_DoorCollider = coll;
            }
        }
    }

    public void OpenDoor()
    {
        m_DetectArea.enabled = false;
        m_DoorCollider.enabled = false;
        m_Anim.SetTrigger("Open");

        StartCoroutine(DisableCeiling());
        m_bIsOpened = true;

        PlayerController.m_instance.m_Interactable = null;
        UIManager.m_instance.SetMessageText("");
    }

    IEnumerator DisableCeiling()
    {
        Material ceilingMaterial = m_Ceiling.GetComponent<MeshRenderer>().material;
        Color ceilingColor = ceilingMaterial.GetColor("_BaseColor");
        
        while (ceilingColor.a > 0)
        {
            //fade out ceiling
            ceilingColor.a -= Time.deltaTime * m_fCeilingFadeSpeed;
            ceilingMaterial.SetColor("_BaseColor", ceilingColor);
            
            yield return null;
        }

        //deactivate it when it's no longer used
        m_Ceiling.SetActive(false);
        yield break;
    }

}
