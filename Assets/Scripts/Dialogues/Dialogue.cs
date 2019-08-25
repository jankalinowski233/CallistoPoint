using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A class to hold data about the dialogue
/// </summary>
[System.Serializable]
[CreateAssetMenu(menuName = "Dialogues/Dialogue")]
public class Dialogue : ScriptableObject
{
    public Sentence[] sentences;
}
