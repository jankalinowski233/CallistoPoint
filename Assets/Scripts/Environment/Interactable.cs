using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class Interactable : MonoBehaviour, IInteractable
{
    public UnityEvent OnInteract;

    public virtual void Interact()
    {
        //base interact behaviour goes here
        OnInteract.Invoke();
        UIManager.m_instance.SetMessageText("");
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
