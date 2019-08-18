using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Details about the sentence
/// </summary>
[System.Serializable]
public class Sentence
{
    public Sprite portrait;
    public string name;
    [TextArea(3, 10)]
    public string dialogueSentences;
    public DialogueEvent sentenceEvents;
}
