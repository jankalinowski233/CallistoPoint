using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    bool m_bCanBeUsed;

    public GameObject m_text;

    private void Awake()
    {
        m_bCanBeUsed = false;
    }

    public virtual void Interact()
    {
        //base interact behaviour goes here
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_bCanBeUsed = true;
            m_text.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (m_bCanBeUsed == true)
            {
                m_bCanBeUsed = false;
                m_text.SetActive(false);
            }
        }
    }
}
