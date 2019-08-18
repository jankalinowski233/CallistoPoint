using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Manages the dialogue system
/// </summary>
public class DialogueManager : MonoBehaviour
{
    //make it a singleton
    public static DialogueManager m_instance;
    
    //queue of sentences
    public Queue<Sentence> m_sentencesQueue;

    //dialogue's UI
    public TextMeshProUGUI m_nameText;
    public TextMeshProUGUI m_dialogueText;
    public GameObject m_dialoguePanel;
    public GameObject m_contButton;

    //characters' portraits
    public Image m_img;

    private void Awake()
    {
        MakeSingleton();
    }

    /// <summary>
    /// Set everything up
    /// </summary>
    void Start()
    {
        m_sentencesQueue = new Queue<Sentence>();
    }

    /// <summary>
    /// Makes it a singleton
    /// </summary>
    void MakeSingleton()
    {
        if (m_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            m_instance = this;
        }
    }

    /// <summary>
    /// Starts the dialogue
    /// </summary>
    /// <param name="d"> dialogue to start </param>
    public void StartDialogue(Dialogue d)
    {
        m_sentencesQueue.Clear();

        foreach(Sentence sentence in d.sentences)
        {
            m_sentencesQueue.Enqueue(sentence);
        }

        ShowPanel();
        ShowNextLine();
    }

    /// <summary>
    /// Shows lines of dialogues
    /// </summary>
    public void ShowNextLine()
    {
        StartCoroutine(DisplayText());
    }

    /// <summary>
    /// Displays text letter by letter
    /// </summary>
    /// <returns></returns>
    public IEnumerator DisplayText()
    {
        m_contButton.SetActive(false);

        if (m_sentencesQueue.Count == 0)
        {
            EndDialogue();
            yield break;
        }

        int size = 0;
        m_dialogueText.text = "";
        m_nameText.text = "";

        Sentence sentence = m_sentencesQueue.Dequeue();
        size = sentence.dialogueSentences.ToCharArray().Length;

        //swap characters' images
        m_img.sprite = sentence.portrait;

        m_nameText.text = sentence.name;

        foreach (char letter in sentence.dialogueSentences.ToCharArray())
        {
            size--;
            m_dialogueText.text += letter;

            if (size == 0)
            {
                m_contButton.SetActive(true);
            }

            yield return null;
        }


        if (sentence.sentenceEvents != null)
           sentence.sentenceEvents.Raise();
        
    }

    /// <summary>
    /// Ends a dialogue
    /// </summary>
    void EndDialogue()
    {
        HidePanel();
    }

    /// <summary>
    /// Shows dialogue panel
    /// </summary>
    public void ShowPanel()
    {
        m_dialoguePanel.SetActive(true);
    }

    /// <summary>
    /// Hides dialogue panel
    /// </summary>
    public void HidePanel()
    {
        m_dialoguePanel.SetActive(false);
    }
}
