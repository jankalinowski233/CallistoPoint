using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class DoorController : MonoBehaviour
{
    bool m_bCanBeUsed;
    bool m_bIsOpened;

    Animator m_Anim;
    BoxCollider m_DetectArea;
    OffMeshLink[] m_MeshLinks;

    public GameObject m_Ceiling;
    public int m_iCeilingFadeSpeed;

    private void Awake()
    {
        m_bCanBeUsed = false;
        m_bIsOpened = false;

        m_Anim = GetComponentInChildren<Animator>();
        m_DetectArea = GetComponent<BoxCollider>();

        m_MeshLinks = GetComponents<OffMeshLink>();
    }

    private void Start()
    {
        foreach(OffMeshLink link in m_MeshLinks)
        {
            link.enabled = false;
        }
    }

    IEnumerator CheckForInput()
    {
        while(m_bCanBeUsed == true)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                OpenDoor();
            }
            
            if(m_bCanBeUsed == false)
            {
                yield break;
            }

            yield return null;
        }
    }

    void OpenDoor()
    {
        m_DetectArea.enabled = false;
        m_Anim.SetTrigger("Open");

        foreach (OffMeshLink link in m_MeshLinks)
        {
            link.enabled = true;
        }

        StartCoroutine(DisableCeiling());
        m_bIsOpened = true;
    }

    IEnumerator DisableCeiling()
    {

        while (true)
        {
            //fade out ceiling

            yield return null;
        }

        m_Ceiling.SetActive(false);
        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(m_bIsOpened == false)
            {
                m_bCanBeUsed = true;

                //enable text "Press [E] to open"

                StartCoroutine(CheckForInput());
            }    
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (m_bCanBeUsed == true)
            {
                m_bCanBeUsed = false;
            }
            else Debug.LogError("Doors are stuck");
        }
    }
}
