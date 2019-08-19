using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class DoorController : Interactable
{
    bool m_bIsOpened;

    Animator m_Anim;
    BoxCollider m_DetectArea;
    BoxCollider m_DoorCollider;
    OffMeshLink[] m_MeshLinks;

    [Header("Ceiling controller")]
    [Space(7f)]
    public GameObject m_Ceiling;
    [Range(1, 10)] [Tooltip("The bigger the number, the slower the fade speed")]
    public int m_iCeilingFadeSpeed;

    private void Awake()
    {
        m_bIsOpened = false;

        m_Anim = GetComponentInChildren<Animator>();
        m_DetectArea = GetComponent<BoxCollider>();

        m_MeshLinks = GetComponents<OffMeshLink>();
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

        foreach(OffMeshLink link in m_MeshLinks)
        {
            link.enabled = false;
        }
    }

    public override void Interact()
    {
        base.Interact();
        OpenDoor();
    }

    void OpenDoor()
    {
        m_DetectArea.enabled = false;
        m_DoorCollider.enabled = false;
        m_Anim.SetTrigger("Open");

        foreach (OffMeshLink link in m_MeshLinks)
        {
            link.enabled = true;
        }

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
            ceilingColor.a -= Time.deltaTime / m_iCeilingFadeSpeed;
            ceilingMaterial.SetColor("_BaseColor", ceilingColor);
            
            yield return null;
        }

        //deactivate it when it's no longer used
        m_Ceiling.SetActive(false);
        yield break;
    }

}
