using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A class to hold data about the dialogue
/// </summary>
[System.Serializable]
[CreateAssetMenu]
public class Dialogue : ScriptableObject
{
    public Sentence[] sentences;
}
