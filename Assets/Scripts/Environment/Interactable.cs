using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour, IInteractable
{
    public UnityEvent OnInteract;

    public virtual void Interact()
    {
        //base interact behaviour goes here
        OnInteract.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.m_instance.SetMessageText("Press [E] to interact");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.m_instance.SetMessageText("");
        }
    }
}
