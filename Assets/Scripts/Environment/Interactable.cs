using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    bool m_bCanBeUsed;
    
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
            UIManager.m_instance.SetMessageText("Press [E] to interact");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (m_bCanBeUsed == true)
            {
                m_bCanBeUsed = false;
                UIManager.m_instance.SetMessageText("");
            }
        }
    }
}
