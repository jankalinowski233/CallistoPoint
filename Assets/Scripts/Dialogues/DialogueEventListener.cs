using UnityEngine;
using UnityEngine.Events;

public class DialogueEventListener : MonoBehaviour
{
    public DialogueEvent m_dialogueEvent;
    public UnityEvent m_dialogueEventResponse;

    public bool m_shouldRemoveListeners = false;

    private void OnEnable()
    {
        if(m_dialogueEvent != null)
            m_dialogueEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        if (m_dialogueEvent != null)
            m_dialogueEvent.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        m_dialogueEventResponse.Invoke();

        if(m_shouldRemoveListeners == true)
        {
            m_dialogueEvent.UnregisterListener(this);
        }
    }
}
