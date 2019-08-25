using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogues/DialogueEvent")]
public class DialogueEvent : ScriptableObject
{
    private List<DialogueEventListener> m_eventListeners = new List<DialogueEventListener>();

    public void Raise()
    {
        if(m_eventListeners.Count > 0)
        {
            m_eventListeners[0].OnEventRaised();
        }
        
    }

    public void RegisterListener(DialogueEventListener listener)
    {
        m_eventListeners.Add(listener);
    }

    public void UnregisterListener(DialogueEventListener listener)
    {
        m_eventListeners.Remove(listener);
    }
}
